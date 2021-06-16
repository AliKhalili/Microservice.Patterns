using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlakyApi.Utils.WeatherApi
{
    public class OpenWeatherMapService : IWeatherService
    {
        private readonly ILogger<OpenWeatherMapService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiKey;
        public OpenWeatherMapService(ILogger<OpenWeatherMapService> logger, IOptions<OpenWeatherOption> options, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _apiKey = options.Value.ApiKey;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                return await response.Content.ReadFromJsonAsync<WeatherResponse>();
            }
            return null;
        }
    }
}