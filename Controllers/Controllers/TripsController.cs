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
            Summary = "Pobiera przejazd o podanym id"
            )]
        [SwaggerResponse(200, "Sukces", typeof(TripResDTO))]
        [SwaggerResponse(400, "Niewłaściwa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono przejazdu", typeof(Exception))]
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
            Summary = "Pobiera przejazdy dla użytkownika"
            )]
        [SwaggerResponse(200, "Sukces", typeof(TripResDTO))]
        [SwaggerResponse(400, "Niewłaściwa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono przejazdów", typeof(Exception))]
        public async Task<ActionResult<IEnumerable<TripResDTO>>> GetTripsForUser()
        {
            var trips = await _tripService.GetTripsForUser();

            if (trips.Count == 0)
                return NotFound(new { message = "No trips for given user" });

            return Ok(trips);
        }

        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Dodaje nowy przejazd (rozpoczęty, ale jeszcze nie zakończony)"
            )]
        [SwaggerResponse(201, "Sukces", typeof(TripResDTO))]
        [SwaggerResponse(400, "Użytkownik miał już aktywny przejazd", typeof(string))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
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
            Summary = "Kończy przejazd użytkownika. Obliczana jest długość przejazdu" +
                      "oraz zwracany jest jego koszt obliczony jako" +
                      "ilość przejechanych minut * 0.10zł."
            )]
        [SwaggerResponse(200, "Sukces", typeof(decimal))]
        [SwaggerResponse(400, "Użytkownik nie ma aktywnego przejazdu", typeof(InvalidOperationException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        public async Task<ActionResult> FinishTrip()
        {
            try
            {
                var cost = await _tripService.FinishTrip();
                return Ok(new { Cost = cost });
            }
            catch(InvalidOperationException exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}
