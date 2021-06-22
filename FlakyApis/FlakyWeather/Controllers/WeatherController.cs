using System;
using System.Threading.Tasks;
using FlakyApi.Utils;
using FlakyApi.Utils.Strategy;
using FlakyApi.Utils.Strategy.CircuitBreaker;
using FlakyApi.Utils.WeatherApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlakyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> Index(string city)
        {
            try
            {
                var result = await _weatherService.GetWeatherAsync(city);
                if (result != null)
                    return Ok(result);
            }
            catch (FlakyServiceHalfOpenException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            catch (FlakyServiceOpenException)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }
}
