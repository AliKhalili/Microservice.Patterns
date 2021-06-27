using System.Threading.Tasks;
using FlakyApi.Implementations;
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
        private readonly IService _service;

        public SystemController(ILogger<SystemController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> Status()
        {
            try
            {
                var result = await _service.DoSomething();
                return Ok(result);
            }
            catch (ServiceCurrentlyUnavailableException e)
            {
                _logger.LogError(e, "service is not available");
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }
    }
}
