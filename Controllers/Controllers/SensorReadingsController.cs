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
            Summary = "Pobiera wszystkie znajdujące się w bazie danych odczyty z sensorów."
        )]
        [SwaggerResponse(200, "Sukces", typeof(IEnumerable<SensorsReadingDTO>))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono żadnych odczytów", typeof(string))]
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

        [SwaggerOperation(Summary = "Pobiera jednie te odczyty z sensorów, które nie były jeszcze nigdy pobrane.")]
        [SwaggerResponse(200, "Sukces", typeof(IEnumerable<SensorsReadingDTO>))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono żadnych odczytów", typeof(string))]
        [HttpGet("new")]
        public async Task<IActionResult> GetNew()
        {
            var newSensorsReadings = await _sensorReadingService.GetAllNewAsync();
            if (!newSensorsReadings.Any())
            {
                return NotFound();
            }
            return Ok(newSensorsReadings);
        }

        [SwaggerOperation(
            Summary = "Zapisuje pojedynczy odczyt do bazy danych." +
                      "W przypadku gdy jednostka w której podany jest odczyt nie jest" +
                      "zgodna z tą przechowywaną w bazie danych, jest ona dostosowana" +
                      "pod warunkiem istnienia odpowiedniego konwertera."
        )]
        [SwaggerResponse(201, "Dodano odczyt", typeof(string))]
        [SwaggerResponse(400, "Struktura zapytania była niewłaściwa", typeof(string))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
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
            Summary = "Zapisuje tablicę odczytów do bazy danych." +
                      "W przypadku gdy jednostka w której podany jest odczyt nie jest" +
                      "zgodna z tą przechowywaną w bazie danych, jest ona dostosowana" +
                      "pod warunkiem istnienia odpowiedniego konwertera."
        )]
        [SwaggerResponse(201, "Dodano odczyty", typeof(string))]
        [SwaggerResponse(400, "Struktura zapytania była niewłaściwa", typeof(string))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
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
            Summary = "Określa czy użytkownik znajduje się w pojeździe komunikacji miejskiej" +
                      "poprzez użycie określonych z góry modeli sieci neuronowych. Jako prawidłowa" +
                      "pod uwagę brana jest odpowiedź o największej \"pewności\"."
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