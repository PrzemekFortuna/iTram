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
    //[Authorize]
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
            Summary = "Getes trip with given id"
            )]
        [SwaggerResponse(200, null, typeof(TripResDTO))]
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
        [SwaggerResponse(200, null, typeof(IEnumerable<TripResDTO>))]
        [SwaggerResponse(404, "No trips for given user", typeof(string))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        public async Task<ActionResult<IEnumerable<TripResDTO>>> GetTripsForUser([FromQuery] int userId)
        {
            var trips = await _tripService.GetTripsForUser(userId);

            if (trips == null)
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
        [SwaggerOperation(Summary = "Finishes trip for user")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "There is no active trip for this user", typeof(InvalidOperationException))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
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
