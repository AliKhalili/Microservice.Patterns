using System.Text.Json.Serialization;

namespace FlakyApi.Implementations
{
    public class DefaultResponse
    {
        [JsonPropertyName("system_status")]
        public string SystemStatus { get; init; }

        [JsonPropertyName("time")]
        public long SystemTicks { get; init; }
    }
}