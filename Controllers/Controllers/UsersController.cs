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
using DBConnection.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [SwaggerOperation(
            Summary = "Pobiera użytkownika o danym id"
         )]
        [SwaggerResponse(200, "Sukces", typeof(User))]
        [SwaggerResponse(400, "Niewłaściwa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono użytkownika", typeof(Exception))]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [SwaggerOperation(
            Summary = "Rejestruje nowego użytkownika"
         )]
        [SwaggerResponse(201, "Sukces", typeof(string))]
        [SwaggerResponse(400, "Niewłaściwa struktura zapytania", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> PostUser([FromBody] User user)
        {
            //TODO: Return bad request when data is invalid or missing
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userService.IfUserExists(user.Email))
                return BadRequest(new { message = "User with given email already exists!" });

            var userDTO = await _userService.RegisterUser(user);

            return CreatedAtAction("PostUser", userDTO);            
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Po podaniu prawidłowego loginu i hasła zwraca token," +
                      "który umożliwia użytkownikowi na wykonywanie akcji," +
                      "które wymagają autoryzacji.")
        ]
        [SwaggerResponse(200, "sukces")]
        [SwaggerResponse(400, "Niewłaściwa sturktura zapytania", typeof(ArgumentNullException))]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.Login(loginDTO.Email, loginDTO.Password);

            if (token == null)
                return BadRequest(new { message = "Wrong email or password" });

            return Ok(token);
        }

    }
}
