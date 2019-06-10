using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBConnection;
using DBConnection.Entities;
using Services;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TramsController : ControllerBase
    {
        private readonly TramService _tramService;

        public TramsController(TramService tramService)
        {
            _tramService = tramService;
        }

        // GET: api/Trams/5
        [SwaggerOperation(
            Summary = "Pobiera pojazd o podanym id."
        )]
        [SwaggerResponse(200, "Znaleziono pojazd", typeof(Tram))]
        [SwaggerResponse(400, "Nieprawidłowa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono pojazdu", typeof(Exception))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tram>> GetTram(int id)
        {
            var tram = await _tramService.GetTram(id);

            if (tram == null)
            {
                return NotFound();
            }

            return Ok(tram);
        }

        // GET: api/Trams?cityId=5
        [SwaggerOperation(
             Summary = "Pobiera wszystkie pojazdy należące do miasta o podanym id"
        )]
        [SwaggerResponse(200, "Znaleziono pojazdy", typeof(IEnumerable<Tram>))]
        [SwaggerResponse(400, "Nieprawidłowa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono pojazdów", typeof(string))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tram>>> GetTramsForCity([FromQuery] int cityId)
        {
            var trams = await _tramService.GetAllTramsForCity(cityId);

            if(trams == null)
            {
                return NotFound();
            }

            return Ok(trams);
        }


        // POST: api/Trams
        [SwaggerOperation(
             Summary = "Tworzy nowy pojazd i zapisuje go w bazie danych"
        )]
        [SwaggerResponse(201, "Utworzono pojazd", typeof(string))]
        [SwaggerResponse(400, "Niewałściwa struktura zapytania", typeof(string))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [HttpPost]
        public async Task<ActionResult<Tram>> PostTram(Tram tram)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tm = await _tramService.AddTram(tram);

            return CreatedAtAction("PostTram", tm);
        }
    }
}
