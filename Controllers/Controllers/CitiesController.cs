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
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityService _cityService;

        public CitiesController(CityService service)
        {
            _cityService = service;
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Pobiera miasto o podanym id."
            )]
        [SwaggerResponse(200, "Znaleziono miasto", typeof(City))]
        [SwaggerResponse(400, "Niewałściwa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono miasta", typeof(Exception))]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            try
            {
                var city = await _cityService.GetCity(id);

                if (city == null)
                {
                    return NotFound(new { message = "City not found" });
                }

                return Ok(city);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Cities
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Tworzy miasto o podanej nazwie i zapisuje je w bazie danych."
            )]
        [SwaggerResponse(201, "Utworzono miasto", typeof(string))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        public async Task<ActionResult<City>> PostCity(string cityName)
        {
            var newCity = await _cityService.AddCity(cityName);

            return CreatedAtAction("PostCity", newCity);
        }
    }
}
