using DBConnection.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Services;

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
            Summary = "Pobiera tokeny na dany dzień. W tym momencie generowane jest 48 tokenów" +
                      "co 24 godziny. Każdy z nich jest ważny przez 30 minut i są zwracane w postaci" +
                      "tablicy bajtów."
        )]
        [SwaggerResponse(200, "Sukces", typeof(IEnumerable<BeaconTokenDto>))]
        [SwaggerResponse(401, "Brak dostępu", typeof(string))]
        [SwaggerResponse(404, "Nie znaleziono tokenów", typeof(string))]
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
