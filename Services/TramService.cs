using DBConnection;
using DBConnection.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TramService
    {
        public TramContext Context { get; set; }

        public TramService(TramContext context)
        {
            Context = context;
        }

        public async Task<Tram> AddTram(Tram tram)
        {
            var tm = await Context.Trams.AddAsync(tram);
            await Context.SaveChangesAsync();

            return tm.Entity;
        }

        public async Task<Tram> GetTram(int id)
        {
            var tm = await Context.Trams.FindAsync(id);
            return tm;        
        }

        public async Task<IEnumerable<Tram>> GetAllTramsForCity(int cityId)
        {
            var trams = await Context.Trams.Where(x => x.CityId == cityId).ToListAsync();

            return trams;
        }
    }
}
