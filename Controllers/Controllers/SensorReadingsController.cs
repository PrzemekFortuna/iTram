using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnection.DTO;
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
            _sensorReadingService.ModelState = ModelState;
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
        public async Task<IActionResult> Post([FromBody] SensorsReadingUnitsDTO sensorsReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _sensorReadingService.AddAsync(sensorsReading);
            if (!res)
                return BadRequest();

            return Created($"api/sensorsreadings/", null);
        }
    }
}