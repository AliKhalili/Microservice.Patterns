using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlakyApi.Utils.WeatherApi
{
    public class FakeWeatherService : IWeatherService
    {
        private readonly ILogger<OpenWeatherMapService> _logger;
        public FakeWeatherService(ILogger<OpenWeatherMapService> logger)
        {
            _logger = logger;
        }
        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            return new()
            {
                Id = DateTime.UtcNow.Ticks,
                Name = city,
                Main = new WeatherResponse.MainProperty()
                {
                    FeelsLike = 1,
                    Humidity = 1,
                    MaximumTemperature = 1,
                    MinimumTemperature = 1,
                    Pressure = 1,
                    Temperature = 1
                }
            };
        }
    }
}