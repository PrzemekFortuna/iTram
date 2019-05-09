using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Handlers;
using Services.Handlers.Accelerometer;
using Services.Handlers.Gyroscope;
using Services.Handlers.Location;
using Services.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace Controllers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddApplicationInsightsTelemetry(Configuration);

            var appSettingsSection = Configuration.GetSection("JWT");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddDbContext<TramContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureDb"), b => b.MigrationsAssembly("DBConnection")), ServiceLifetime.Transient);
            services.AddEntityFrameworkNpgsql().AddDbContext<TramContext>().BuildServiceProvider();
            services.AddTransient<UserService>();
            services.AddTransient<SensorReadingService>();
            services.AddTransient<TramService>();
            services.AddTransient<TripService>();
            services.AddSingleton(AutoMapperConfiguration.Initialize());
            services.AddSingleton(CreateAccelerometerHandlers());
            services.AddSingleton(CreateLocationHandler());
            services.AddSingleton(CreateGyroscopeHandlers());
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "iTram API", Description = "REST API for intelligent tram project" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "iTram API");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }

        private LocationHandler CreateLocationHandler()
        {
            var ret = new DecimalDegreesHandler();
            ret.SetNext(new DegreesMinutesSecondsHandler());
            return ret;
        }

        private AccelerometerHandler CreateAccelerometerHandlers()
        {
            var ret = new MeterPerSecondSquaredHandler();
            return ret;
        }

        private GyroscopeHandler CreateGyroscopeHandlers()
        {
            var ret = new RadPerSHandler();
            ret.SetNext(new RpmHandler());
            return ret;
        }
    }
}
