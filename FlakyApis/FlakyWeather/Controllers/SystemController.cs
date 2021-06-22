using System.Threading.Tasks;
using FlakyApi.Implementation;
using FlakyApi.Utils.Strategy.CircuitBreaker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlakyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private readonly IFlakyService _flakyService;

        public SystemController(ILogger<SystemController> logger, IFlakyService flakyService)
        {
            _logger = logger;
            _flakyService = flakyService;
        }

        [HttpGet]
        [Route("[status]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> Status()
        {
            var result = await _flakyService.RetrieveSystemStatus();
            if (result)
                return Ok();
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}
