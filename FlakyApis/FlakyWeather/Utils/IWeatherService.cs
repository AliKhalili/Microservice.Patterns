using System.Threading.Tasks;
using FlakyApi.Utils.WeatherApi;

namespace FlakyApi.Utils
{
    /// <summary>
    /// Access current weather data for any location on Earth
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// get current weather data by city name
        /// </summary>
        /// <param name="city">city name</param>
        /// <returns>A <see cref="WeatherResponse"/> if api call is successful, otherwise <c>null</c></returns>
        Task<WeatherResponse> GetWeatherAsync(string city);
    }
}