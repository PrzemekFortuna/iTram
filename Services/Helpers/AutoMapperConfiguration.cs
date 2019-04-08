using AutoMapper;
using DBConnection.DTO;
using DBConnection.Entities;

namespace Services.Helpers
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<User, LoginDTO>();
                cfg.CreateMap<Trip, TripDTO>();
                cfg.CreateMap<SensorsReading, SensorsReadingDTO>()
                    .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Location.Latitude))
                    .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Location.Longitude))
                    .ForMember(dest => dest.Ax, opts => opts.MapFrom(src => src.Accelerometer.Ax))
                    .ForMember(dest => dest.Ay, opts => opts.MapFrom(src => src.Accelerometer.Ay))
                    .ForMember(dest => dest.Az, opts => opts.MapFrom(src => src.Accelerometer.Az))
                    .ForMember(dest => dest.Gx, opts => opts.MapFrom(src => src.Gyroscope.Gx))
                    .ForMember(dest => dest.Gy, opts => opts.MapFrom(src => src.Gyroscope.Gy))
                    .ForMember(dest => dest.Gz, opts => opts.MapFrom(src => src.Gyroscope.Gz))
                    .ReverseMap();
                cfg.CreateMap<SensorsReading, SensorsReadingUnitsDTO>()
                    .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Location.Latitude))
                    .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Location.Longitude))
                    .ForMember(dest => dest.LocationUnit, opts => opts.MapFrom(src => src.Location.LocationUnit))
                    .ForMember(dest => dest.Ax, opts => opts.MapFrom(src => src.Accelerometer.Ax))
                    .ForMember(dest => dest.Ay, opts => opts.MapFrom(src => src.Accelerometer.Ay))
                    .ForMember(dest => dest.Az, opts => opts.MapFrom(src => src.Accelerometer.Az))
                    .ForMember(dest => dest.AccelerometerUnit, opts => opts.MapFrom(src => src.Accelerometer.AccelerometerUnit))
                    .ForMember(dest => dest.Gx, opts => opts.MapFrom(src => src.Gyroscope.Gx))
                    .ForMember(dest => dest.Gy, opts => opts.MapFrom(src => src.Gyroscope.Gy))
                    .ForMember(dest => dest.Gz, opts => opts.MapFrom(src => src.Gyroscope.Gz))
                    .ForMember(dest => dest.GyroscopeUnit, opts => opts.MapFrom(src => src.Gyroscope.GyroscopeUnit))
                    .ReverseMap();

            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}



