using DBConnection;
using DBConnection.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        public TramContext Context { get; set; }

        public UserService(TramContext context)
        {
            Context = context;
        }


        public async Task<User> GetUser(int userId)
        {
            var user = await Context.Users.FindAsync(userId);

            return user;
        }
    }
}
