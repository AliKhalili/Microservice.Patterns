using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FlakyWeather.Utils.WeatherApi
{
    public class WeatherResponse
    {
        [JsonPropertyName("main")]
        public MainProperty Main { get; set; }
        [JsonPropertyName("wind")]
        public WindProperty Wind { get; set; }
        [JsonPropertyName("sys")]
        public SysProperty System { get; set; }
        [JsonPropertyName("weather")]
        public IEnumerable<WeatherProperty> Weather { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public class MainProperty
        {
            [JsonPropertyName("temp")]
            public float Temperature { get; set; }
            [JsonPropertyName("feels_like")]
            public float FeelsLike { get; set; }
            [JsonPropertyName("temp_min")]
            public float MinimumTemperature { get; set; }
            [JsonPropertyName("temp_max")]
            public float MaximumTemperature { get; set; }
            [JsonPropertyName("pressure")]
            public int Pressure { get; set; }
            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }
        }

        public class WindProperty
        {
            [JsonPropertyName("speed")]
            public float Speed { get; set; }
            [JsonPropertyName("deg")]
            public int Deg { get; set; }
            [JsonPropertyName("gust")]
            public float Gust { get; set; }
        }

        public class SysProperty
        {
            [JsonPropertyName("type")]
            public int Type { get; set; }
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("country")]
            public string Country { get; set; }
            [JsonPropertyName("sunrise")]
            public int Sunrise { get; set; }
            [JsonPropertyName("sunset")]
            public int Sunset { get; set; }
        }
        public class WeatherProperty
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("main")]
            public string Main { get; set; }
            [JsonPropertyName("description")]
            public string Description { get; set; }
            [JsonPropertyName("icon")]
            public string Icon { get; set; }
        }
    }
}