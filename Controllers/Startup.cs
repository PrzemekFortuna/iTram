using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddDbContext<TramContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureDb"), b => b.MigrationsAssembly("DBConnection")), ServiceLifetime.Transient);
            services.AddTransient<UserService>();
            services.AddTransient<SensorReadingService>();
            services.AddTransient<TramService>();
            services.AddSingleton(AutoMapperConfiguration.Initialize());
            services.AddSingleton(CreateAccelerometerHandlers());
            services.AddSingleton(CreateLocationHandler());
            services.AddSingleton(CreateGyroscopeHandlers());
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
