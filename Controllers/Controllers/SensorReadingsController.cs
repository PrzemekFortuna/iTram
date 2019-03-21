using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnection.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorReadingsController : ControllerBase
    {
        private readonly SensorReadingService _sensorReadingService;

        public SensorReadingsController(SensorReadingService sensorReadingService)
        {
            _sensorReadingService = sensorReadingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sensorsReadings = await _sensorReadingService.GetAllAsync();
            if (!sensorsReadings.Any())
            {
                return NotFound();
            }
            return Ok(sensorsReadings);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] SensorsReading sensorsReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _sensorReadingService.AddAsync(sensorsReading);
            return Created($"api/sensorsreadings/{sensorsReading.Id}", null);

            //todo fix SensorsReading entity, add migration
        }
    }
}