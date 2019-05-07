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
            Summary = "Gets tram with given ID"
        )]
        [SwaggerResponse(200, "Tram was found", typeof(Tram))]
        [SwaggerResponse(400, "No id provided", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "Tram not found", typeof(Exception))]
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
             Summary = "Gets trams from given city"
        )]
        [SwaggerResponse(200, "Trams were found", typeof(IEnumerable<Tram>))]
        [SwaggerResponse(400, "No city id provided", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "Trams not found", typeof(string))]
        [HttpGet("{id}")]
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
             Summary = "Saves tram to database"
        )]
        [SwaggerResponse(201, "Trams were found", typeof(string))]
        [SwaggerResponse(400, "Request structure was wrong", typeof(string))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
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
