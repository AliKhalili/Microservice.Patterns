namespace FlakyApi.Implementations
{
    public class FlakyStrategyOptions
    {
        public static string ConfigSection = "FlakyStrategy";
        public int FirstEventOccurrenceTimeStep { get; set; }
        public int TimeStepInterval { get; set; }
    }
}