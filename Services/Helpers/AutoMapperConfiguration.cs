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
                cfg.CreateMap<Trip, TripResDTO>();
                cfg.CreateMap<SensorsReading, SensorsReadingDTO>()
                    .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Location.Latitude))
                    .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Location.Longitude))
                    .ForMember(dest => dest.Ax, opts => opts.MapFrom(src => src.Accelerometer.Ax))
                    .ForMember(dest => dest.Ay, opts => opts.MapFrom(src => src.Accelerometer.Ay))
                    .ForMember(dest => dest.Az, opts => opts.MapFrom(src => src.Accelerometer.Az))
                    .ForMember(dest => dest.Gx, opts => opts.MapFrom(src => src.Gyroscope.Gx))
                    .ForMember(dest => dest.Gy, opts => opts.MapFrom(src => src.Gyroscope.Gy))
                    .ForMember(dest => dest.Gz, opts => opts.MapFrom(src => src.Gyroscope.Gz))
                    .ForMember(dest => dest.GameRotationVecX, opts => opts.MapFrom(src => src.GameRotation.GameRotationVecX))
                    .ForMember(dest => dest.GameRotationVecY, opts => opts.MapFrom(src => src.GameRotation.GameRotationVecY))
                    .ForMember(dest => dest.GameRotationVecZ, opts => opts.MapFrom(src => src.GameRotation.GameRotationVecZ))
                    .ForMember(dest => dest.GeomagneticRotationVecX, opts => opts.MapFrom(src => src.GeomagneticRotation.GeomagneticRotationVecX))
                    .ForMember(dest => dest.GeomagneticRotationVecY, opts => opts.MapFrom(src => src.GeomagneticRotation.GeomagneticRotationVecY))
                    .ForMember(dest => dest.GeomagneticRotationVecZ, opts => opts.MapFrom(src => src.GeomagneticRotation.GeomagneticRotationVecZ))
                    .ForMember(dest => dest.MagneticFieldX, opts => opts.MapFrom(src => src.MagneticField.MagneticFieldX))
                    .ForMember(dest => dest.MagneticFieldY, opts => opts.MapFrom(src => src.MagneticField.MagneticFieldY))
                    .ForMember(dest => dest.MagneticFieldZ, opts => opts.MapFrom(src => src.MagneticField.MagneticFieldZ))
                    .ForMember(dest => dest.GravityX, opts => opts.MapFrom(src => src.Gravity.GravityX))
                    .ForMember(dest => dest.GravityY, opts => opts.MapFrom(src => src.Gravity.GravityY))
                    .ForMember(dest => dest.GravityZ, opts => opts.MapFrom(src => src.Gravity.GravityZ))
                    .ForMember(dest => dest.RotationVecX, opts => opts.MapFrom(src => src.RotationVec.RotationVecX))
                    .ForMember(dest => dest.RotationVecY, opts => opts.MapFrom(src => src.RotationVec.RotationVecY))
                    .ForMember(dest => dest.RotationVecZ, opts => opts.MapFrom(src => src.RotationVec.RotationVecZ))
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
                    .ForMember(dest => dest.GameRotationVecX, opts => opts.MapFrom(src => src.GameRotation.GameRotationVecX))
                    .ForMember(dest => dest.GameRotationVecY, opts => opts.MapFrom(src => src.GameRotation.GameRotationVecY))
                    .ForMember(dest => dest.GameRotationVecZ, opts => opts.MapFrom(src => src.GameRotation.GameRotationVecZ))
                    .ForMember(dest => dest.GeomagneticRotationVecX, opts => opts.MapFrom(src => src.GeomagneticRotation.GeomagneticRotationVecX))
                    .ForMember(dest => dest.GeomagneticRotationVecY, opts => opts.MapFrom(src => src.GeomagneticRotation.GeomagneticRotationVecY))
                    .ForMember(dest => dest.GeomagneticRotationVecZ, opts => opts.MapFrom(src => src.GeomagneticRotation.GeomagneticRotationVecZ))
                    .ForMember(dest => dest.MagneticFieldX, opts => opts.MapFrom(src => src.MagneticField.MagneticFieldX))
                    .ForMember(dest => dest.MagneticFieldY, opts => opts.MapFrom(src => src.MagneticField.MagneticFieldY))
                    .ForMember(dest => dest.MagneticFieldZ, opts => opts.MapFrom(src => src.MagneticField.MagneticFieldZ))
                    .ForMember(dest => dest.GravityX, opts => opts.MapFrom(src => src.Gravity.GravityX))
                    .ForMember(dest => dest.GravityY, opts => opts.MapFrom(src => src.Gravity.GravityY))
                    .ForMember(dest => dest.GravityZ, opts => opts.MapFrom(src => src.Gravity.GravityZ))
                    .ForMember(dest => dest.RotationVecX, opts => opts.MapFrom(src => src.RotationVec.RotationVecX))
                    .ForMember(dest => dest.RotationVecY, opts => opts.MapFrom(src => src.RotationVec.RotationVecY))
                    .ForMember(dest => dest.RotationVecZ, opts => opts.MapFrom(src => src.RotationVec.RotationVecZ))
                    .ReverseMap();

            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}



