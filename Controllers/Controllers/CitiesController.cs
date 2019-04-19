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
            Summary = "Gets city with given ID"
            )]
        [SwaggerResponse(200, null, typeof(City))]
        [SwaggerResponse(400, "No name provided", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        [SwaggerResponse(404, "City not found", typeof(Exception))]
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
            Summary = "Gets city with given ID"
            )]
        [SwaggerResponse(201, null, typeof(City))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        [SwaggerResponse(404, "City not found", typeof(Exception))]
        public async Task<ActionResult<City>> PostCity(string cityName)
        {
            var newCity = await _cityService.AddCity(cityName);

            return CreatedAtAction("PostCity", newCity);
        }
    }
}
