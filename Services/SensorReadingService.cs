using System.Collections.Generic;
using System.Threading.Tasks;
using DBConnection;
using DBConnection.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class SensorReadingService
    {
        public TramContext Context { get; set; }

        public SensorReadingService(TramContext context)
        {
            Context = context;
        }


        public async Task<IEnumerable<SensorsReading>> GetAllAsync()
        {
            var sensorReadings = await Context.SensorsReadings.ToListAsync();
            return sensorReadings;
        }

        public async Task AddAsync(SensorsReading sensorsReading)
        {
            await Context.SensorsReadings.AddAsync(sensorsReading);
        }
    }
}