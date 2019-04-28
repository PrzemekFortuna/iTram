using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnection.DTO;
using DBConnection.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(
            Summary = "Gets all sensors' readings"
        )]
        [SwaggerResponse(200, null, typeof(IEnumerable<SensorsReadingDTO>))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        [SwaggerResponse(404, "no readings found", typeof(string))]
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

        [SwaggerOperation(
            Summary = "Gets all sensors' readings"
        )]
        [SwaggerResponse(201, "Sensor's added", typeof(string))]
        [SwaggerResponse(400, "Request structure was wrongs", typeof(string))]
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