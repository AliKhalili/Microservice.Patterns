using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlakyWeather.Utils;
using FlakyWeather.Utils.WeatherApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FlakyWeather.Controllers
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
            var result = await _weatherService.GetWeatherAsync(city);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}
