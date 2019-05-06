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
            Summary = "Gets user with given ID"
         )]
        [SwaggerResponse(200, "User was found", typeof(User))]
        [SwaggerResponse(400, "No id provided", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "User not found", typeof(Exception))]
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
            Summary = "Registers new user"
         )]
        [SwaggerResponse(201, "User was created", typeof(string))]
        [SwaggerResponse(400, "Request structure was wrong", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
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
            Summary = "Grants JWT token")
        ]
        [SwaggerResponse(200, "JWT token")]
        [SwaggerResponse(400, "Request structure was wrong", typeof(ArgumentNullException))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
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
