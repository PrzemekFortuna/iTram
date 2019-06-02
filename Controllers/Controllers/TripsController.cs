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
using Swashbuckle.AspNetCore.Annotations;

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
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Gets trip with given ID"
            )]
        [SwaggerResponse(200, "Trip was found", typeof(TripResDTO))]
        [SwaggerResponse(400, "No id provided", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "Trip not found", typeof(Exception))]
        public async Task<ActionResult<TripResDTO>> GetTrip(int id)
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
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Gets trips for user"
            )]
        [SwaggerResponse(200, "Trips were found", typeof(IEnumerable<TripResDTO>))]
        [SwaggerResponse(400, "No user id provided", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "No trips for given user", typeof(string))]
        public async Task<ActionResult<IEnumerable<TripResDTO>>> GetTripsForUser([FromQuery] int userId)
        {
            var trips = await _tripService.GetTripsForUser(userId);

            if (trips.Count == 0)
                return NotFound(new { message = "No trips for given user" });

            return Ok(trips);
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Adds trip"
            )]
        [SwaggerResponse(201, "Trip created", typeof(TripResDTO))]
        [SwaggerResponse(400, "There is already an active trip for this user", typeof(string))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        public async Task<ActionResult<Trip>> PostTrip(TripReqDTO tripDTO)
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
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Finishes trip for user and returns its cost"
            )]
        [SwaggerResponse(200, "Trip finished", typeof(decimal))]
        [SwaggerResponse(400, "There is no active trip for this user", typeof(InvalidOperationException))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        public async Task<ActionResult> FinishTrip([FromQuery] int userId)
        {
            try
            {
                var cost = await _tripService.FinishTrip(userId);
                return Ok(new { Cost = cost });
            }
            catch(InvalidOperationException exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}
