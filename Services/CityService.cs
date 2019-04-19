using DBConnection;
using DBConnection.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CityService
    {
        private TramContext _context;

        public CityService(TramContext context)
        {
            _context = context;
        }

        public async Task<City> AddCity(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("No name provided");

            var city = await _context.Cities.AddAsync(new City() { Name = name });

            await _context.SaveChangesAsync();

            return city.Entity;
        }

        public async Task<City> GetCity(int id)
        {
            var city = await _context.Cities.Include(c => c.Trams).FirstOrDefaultAsync(c => c.Id == id);
            return city;
        }
    }
}
