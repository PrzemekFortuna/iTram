using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBConnection;
using DBConnection.DTO;
using DBConnection.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Services.Handlers;
using Services.Handlers.Gyroscope;
using Services.Helpers;

namespace Services
{
    public class SensorReadingService
    {
        private readonly IMapper _mapper;
        private readonly AccelerometerHandler _accelerometerHandler;
        private readonly GyroscopeHandler _gyroscopeHandler;
        private readonly LocationHandler _locationHandler;
        public TramContext Context { get; set; }
        public ModelStateDictionary ModelState;
        public SensorReadingService(TramContext context, IMapper mapper, AccelerometerHandler accelerometerHandler,
            GyroscopeHandler gyroscopeHandler, LocationHandler locationHandler)
        {
            _mapper = mapper;
            _accelerometerHandler = accelerometerHandler;
            _gyroscopeHandler = gyroscopeHandler;
            _locationHandler = locationHandler;
            Context = context;
        }


        public async Task<IEnumerable<SensorsReadingDTO>> GetAllAsync()
        {
            var sensorReadings = await Context.SensorsReadings
                .Include(x => x.Gyroscope)
                .Include(x => x.Accelerometer)
                .Include(x => x.Location)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SensorsReadingDTO>>(sensorReadings);
        }

        public async Task<bool> AddAsync(SensorsReadingUnitsDTO sensorsReading)
        {
            var reading = _mapper.Map<SensorsReading>(sensorsReading);
            if (!String.IsNullOrWhiteSpace(reading.Accelerometer.AccelerometerUnit))
            {
                var result = _accelerometerHandler.Handle(reading.Accelerometer);
                if (result == null)
                    return false;
            }
            if (!String.IsNullOrWhiteSpace(reading.Gyroscope.GyroscopeUnit))
            {
                var result = _gyroscopeHandler.Handle(reading.Gyroscope);
                if (result == null)
                    return false;
            }
            if (!String.IsNullOrWhiteSpace(reading.Location.LocationUnit))
            {
                var result = _locationHandler.Handle(reading.Location);
                if (result == null)
                    return false;
            }
            reading.TimeStamp = DateTime.Now;
            reading.UserId = 1; // todo: once authorization works, fetch id from session
            var sr = await Context.SensorsReadings.AddAsync(reading);
            await Context.SaveChangesAsync();

            return true;
        }


    }
}