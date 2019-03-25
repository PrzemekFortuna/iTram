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

        // GET: api/Users/5
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
                return NotFound();
            }

            return Ok(user);
        }

        /// POST
        /// <route> api/Users/register </route>
        /// <summary>
        /// Register endpoint. Receives User object in request body. Endpoint is allowed anonymously - no JWT token is required
        /// Returned codes:
        /// 400 Bad Request - when user already exists or provided data is invalid
        /// 201 Created
        /// </summary>
        /// <param name="user"></param>
        /// <returns> UserDTO </returns>
        [AllowAnonymous]
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

        /// POST
        /// <route> api/Users/login </route>
        /// <summary>
        /// Login endpoint. Receives LoginDTO object in body and if provided credentials are correct, returns JWT token.
        /// Endpoint is allowed anonymously - no JWT token is required
        /// Returned codes:
        /// 400 Bad Request - wrong email or password
        /// 200 Ok - when provided credentials are correct and JWT token is returned
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns> string JWT token </returns>
        [AllowAnonymous]
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
