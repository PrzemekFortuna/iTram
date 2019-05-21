using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnection.DTO;
using DBConnection.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [SwaggerResponse(200, "Success", typeof(IEnumerable<SensorsReadingDTO>))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "No readings were found", typeof(string))]
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
            Summary = "Save one sensors' reading to database"
        )]
        [SwaggerResponse(201, "Sensors' reading added", typeof(string))]
        [SwaggerResponse(400, "Request structure was wrong", typeof(string))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> PostSensorsReading([FromBody] SensorsReadingUnitsDTO sensorsReading)
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

        [SwaggerOperation(
            Summary = "Save one or more sensors' reading to database"
        )]
        [SwaggerResponse(201, "Sensors' readings added", typeof(string))]
        [SwaggerResponse(400, "Request structure was wrong", typeof(string))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [HttpPost("multiple-new")]
        [Authorize]
        public async Task<IActionResult> PostSensorsReadings([FromBody] IEnumerable<SensorsReadingUnitsDTO> sensorsReadings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _sensorReadingService.AddAllAsync(sensorsReadings);
            if (!res)
                return BadRequest();

            return Created($"api/sensorsreadings/", null);
        }


        [SwaggerOperation(
            Summary = "Verify whether user is in tram"
        )]
        [SwaggerResponse(200, "Reply returned", typeof(string))]
        [SwaggerResponse(400, "Request structure was wrong", typeof(string))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [HttpPost("amiintram")]
        [Authorize]
        public async Task<IActionResult> AmIInTram([FromBody] IEnumerable<SensorsReadingUnitsDTO> sensorsReadings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _sensorReadingService.AmIInTram(sensorsReadings);
            if (res == null)
                return BadRequest();

            return Ok(new { AmIInTram = res.Value });
        }
    }
}