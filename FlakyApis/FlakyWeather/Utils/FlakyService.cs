using System.Collections.Generic;
using System.Threading.Tasks;
using FlakyWeather.Utils.WeatherApi;
using Microsoft.Extensions.Logging;

namespace FlakyWeather.Utils
{
    public class FlakyWeatherService : IWeatherService
    {
        private readonly ILogger<FlakyWeatherService> _logger;
        private IFlakyStrategy _flakyStrategy;
        private IWeatherService _weatherService;

        public FlakyWeatherService(ILogger<FlakyWeatherService> logger, IFlakyStrategy flakyStrategy, OpenWeatherMapService weatherService)
        {
            _logger = logger;
            _flakyStrategy = flakyStrategy;
            _weatherService = weatherService;
        }


        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            return await _flakyStrategy.Execute(
                async ctx => await _weatherService.GetWeatherAsync(ctx["city"].ToString()),
                new Dictionary<string, object> { { "city", city } });
        }
    }
}