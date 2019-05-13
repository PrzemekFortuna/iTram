using DBConnection.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BeaconTokenController : ControllerBase
    {
        private readonly BeaconTokenService _beaconTokenService;

        public BeaconTokenController(BeaconTokenService beaconTokenService)
        {
            _beaconTokenService = beaconTokenService;
        }

        [SwaggerOperation(
            Summary = "Get beacon tokens for current day"
        )]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<BeaconTokenDto>))]
        [SwaggerResponse(401, "Unauthorized access", typeof(string))]
        [SwaggerResponse(404, "No beacon tokens were found", typeof(string))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var beaconTokens = await _beaconTokenService.GetActiveBeaconTokens();
            if (!beaconTokens.Any())
            {
                return NotFound();
            }
            return Ok(beaconTokens);
        }
    }
}
