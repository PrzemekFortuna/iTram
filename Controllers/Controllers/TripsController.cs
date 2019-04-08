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
using DBConnection.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripsController(TripService service)
        {
            _tripService = service;
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            var trip = await _tripService.GetTrip(id);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // GET: api/Trips?userId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTripsForUser([FromQuery] int userId)
        {
            var trips = await _tripService.GetTripsForUser(userId);

            if (trips == null)
                return NotFound(new { message = "No trips for give user" });

            return Ok(trips);
        }

        // POST: api/Trips
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(TripDTO tripDTO)
        {
            try
            {
                var trip = await _tripService.CreateTrip(tripDTO);
                return CreatedAtAction("PostTrip", trip);
            }
            catch (InvalidOperationException exp)
            {
                return BadRequest(exp.Message);
            }
        }

        // PATCH: api/Trips?userId=5
        [HttpPatch("finish")]
        public async Task<ActionResult> FinishTrip([FromQuery] int userId)
        {
            try
            {
                await _tripService.FinishTrip(userId);
                return Ok();
            }
            catch(InvalidOperationException exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}
