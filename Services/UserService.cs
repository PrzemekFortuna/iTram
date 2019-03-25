using DBConnection;
using DBConnection.DTO;
using DBConnection.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        public TramContext Context { get; set; }
        private readonly AppSettings _appSettings;

        public UserService(TramContext context, IOptions<AppSettings> appSettings)
        {
            Context = context;
            _appSettings = appSettings.Value;
        }


        public async Task<User> GetUser(int userId)
        {
            var user = await Context.Users.FindAsync(userId);

            return user;
        }

        //Register
        public async Task<UserDTO> RegisterUser(User user)
        {
            user.Password = GenerateHash(user.Password);

            var result = await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();

            var userDTO = new UserDTO()
            {
                UserId = result.Entity.UserId,
                Email = result.Entity.Email,
                Name = result.Entity.Name,
                Lastname = result.Entity.Lastname
            };

            return userDTO;
        }

        //Login
        public async Task<string> Login(string email, string password)
        {
            //TODO: validate if email, passwords are not empty
            var hash = GenerateHash(password);            
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == hash);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GenerateHash(string value)
        {
            //TODO: Add salt
            using (var sha = SHA512.Create())
            {
                var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(value));

                return BitConverter.ToString(hash);
            }
        }

        public bool IfUserExists(string email)
        {
            return Context.Users.Any(u => u.Email == email);
        }
    }
}
