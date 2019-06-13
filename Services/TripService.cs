using AutoMapper;
using DBConnection;
using DBConnection.DTO;
using DBConnection.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class TripService
    {
        private static decimal costPerMinute = 0.1m;
        private TramContext _context;
        private IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TripService(TramContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TripResDTO> CreateTrip(TripReqDTO tripDTO)
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var tripExists = await ActiveTripForUserExists(userId);
            if (tripExists)
                throw new InvalidOperationException("There is already an active trip for user");

            var trip = new Trip()
            {
                IsFinished = false,
                StartTime = DateTime.Now,
                TramId = tripDTO.TramId,
                UserId = userId,
                Length = 0f
            };
            var tr = await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();

            return _mapper.Map<TripResDTO>(tr.Entity);
        }



        public async Task<ICollection<TripResDTO>> GetTripsForUser()
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var trips = await _context.Trips.Where(t => t.UserId == userId)
                .Select(tr => _mapper.Map<TripResDTO>(tr)).ToListAsync();

            return trips;
        }

        public async Task<TripResDTO> GetTrip(int id)
        {
            var tr = await _context.Trips.FindAsync(id);
            return _mapper.Map<TripResDTO>(tr);
        }

        public async Task<decimal> FinishTrip()
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var tripExists = await ActiveTripForUserExists(userId);
            if (!tripExists)
                throw new InvalidOperationException("There is no active trip for this user");

            var trip = await _context.Trips.SingleAsync(t => t.UserId == userId && t.IsFinished == false);
            trip.IsFinished = true;
            trip.FinishTime = DateTime.Now;

            var cost = (trip.FinishTime - trip.StartTime).Minutes * costPerMinute;
            await _context.SaveChangesAsync();

            return cost;
        }

        private async Task<bool> ActiveTripForUserExists(int userId)
        {
            var tr = await _context.Trips.AnyAsync(t => t.UserId == userId && t.IsFinished == false);

            return tr;
        }

        private void ThrowActiveTripExists()
        {
            throw new InvalidOperationException("There is already an active trip for user");
        }

    }
}
