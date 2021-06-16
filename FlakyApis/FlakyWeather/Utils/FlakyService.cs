using System.Collections.Generic;
using System.Threading.Tasks;
using FlakyApi.Utils.WeatherApi;
using Microsoft.Extensions.Logging;

namespace FlakyApi.Utils
{
    public class FlakyWeatherService : IWeatherService
    {
        private readonly ILogger<FlakyWeatherService> _logger;
        private IFlakyStrategy _flakyStrategy;
        private IWeatherService _weatherService;

        public FlakyWeatherService(ILogger<FlakyWeatherService> logger, IFlakyStrategy flakyStrategy, FakeWeatherService weatherService)
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