using System.Text.Json.Serialization;

namespace FlakyApi.Implementations
{
    public class FlakyStrategyOptions
    {
        [JsonIgnore]
        public static string ConfigSection = "FlakyStrategy";
        
        [JsonPropertyName("time_of_first_failure")]
        public int FirstEventOccurrenceTimeStep { get; set; }
        
        [JsonPropertyName("total_time_of_interval")]
        public int TimeStepInterval { get; set; }
    }
}