using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DBConnection;
using DBConnection.DTO;
using DBConnection.Entities;
using DBConnection.Entities.Sensors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Savage.Measurements.UnitsOfMeasure;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ModelsManager.ModelsManager _modelsManager;
        private readonly IOptions<LocationSimilaritySettings> _locationSimilaritySettings;
        public TramContext Context { get; set; }
        public ModelStateDictionary ModelState;

        public SensorReadingService(TramContext context, IMapper mapper, AccelerometerHandler accelerometerHandler,
            GyroscopeHandler gyroscopeHandler, LocationHandler locationHandler,
            IHttpContextAccessor httpContextAccessor,
            ModelsManager.ModelsManager modelsManager, IOptions<LocationSimilaritySettings> locationSimilaritySettings)
        {
            _mapper = mapper;
            _accelerometerHandler = accelerometerHandler;
            _gyroscopeHandler = gyroscopeHandler;
            _locationHandler = locationHandler;
            _httpContextAccessor = httpContextAccessor;
            _modelsManager = modelsManager;
            _locationSimilaritySettings = locationSimilaritySettings;
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

            if (!TryToHandleSensorsReading(reading))
                return false;

            var sr = await Context.SensorsReadings.AddAsync(reading);
            await Context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> AddAllAsync(IEnumerable<SensorsReadingUnitsDTO> sensorsReadings)
        {
            var readings = _mapper.Map<IEnumerable<SensorsReading>>(sensorsReadings).ToList();
            foreach (var sensorsReading in readings)
            {
                if (!TryToHandleSensorsReading(sensorsReading))
                    return false;

            }

            await Context.AddRangeAsync(readings);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool?> AmIInTram(IEnumerable<SensorsReadingUnitsDTO> sensorsReadings, bool useNeuralNetwork,
            bool useLocation)
        {
            var readings = _mapper.Map<IEnumerable<SensorsReading>>(sensorsReadings).ToList();
            foreach (var sensorsReading in readings)
            {
                if (!TryToHandleSensorsReading(sensorsReading))
                    return null;
            }

            var neuralReply = useNeuralNetwork ? await _modelsManager.IsInTram(readings) : true ;
            var gpsSimilarity = useLocation ? IsLocationSimilar(readings) : true;

            return neuralReply && gpsSimilarity;
        }

        private bool IsLocationSimilar(List<SensorsReading> readings)
        {
            var match = 0;
            foreach (var reading in readings)
            {

                var mobilePhoneLocation = new Savage.GPS.Position(
                    double.Parse(reading.Location.Longitude, CultureInfo.InvariantCulture),
                    double.Parse(reading.Location.Latitude, CultureInfo.InvariantCulture));
                var vehicleLocation = new Savage.GPS.Position(
                    double.Parse(reading.VehicleLocation.VehicleLongitude, CultureInfo.InvariantCulture),
                    double.Parse(reading.VehicleLocation.VehicleLatitude, CultureInfo.InvariantCulture));


                var distance = mobilePhoneLocation.DistanceFrom(vehicleLocation);

                if (distance.Convert(Distances.Meters).Value <= _locationSimilaritySettings.Value.DistanceInMeters)
                    match++;
            }

            return 1.0 * match / readings.Count * 100 >= _locationSimilaritySettings.Value.MatchPercent;
        }



        private bool TryToHandleSensorsReading(SensorsReading sensorsReading)
        {
            if (!String.IsNullOrWhiteSpace(sensorsReading.Accelerometer.AccelerometerUnit))
            {
                var result = _accelerometerHandler.Handle(sensorsReading.Accelerometer);
                if (result == null)
                    return false;
            }
            if (!String.IsNullOrWhiteSpace(sensorsReading.Gyroscope.GyroscopeUnit))
            {
                var result = _gyroscopeHandler.Handle(sensorsReading.Gyroscope);
                if (result == null)
                    return false;
            }
            if (!String.IsNullOrWhiteSpace(sensorsReading.Location.LocationUnit))
            {
                var result = _locationHandler.Handle(sensorsReading.Location);
                if (result == null)
                    return false;
            }

            sensorsReading.UserId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            return true;
        }

        public async Task<IEnumerable<SensorsReadingDTO>> GetAllNewAsync()
        {
            var sensorReadings = await Context.SensorsReadings
                .Where(x => x.IsNew)
                .Include(x => x.Gyroscope)
                .Include(x => x.Accelerometer)
                .Include(x => x.Location)
                .ToListAsync();

            sensorReadings.ForEach(x => x.IsNew = false);
            await Context.SaveChangesAsync();

            return _mapper.Map<IEnumerable<SensorsReadingDTO>>(sensorReadings);
        }
    }
}