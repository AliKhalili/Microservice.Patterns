namespace FlakyWeather.Utils.Strategy
{
    public enum CircuitBreakerStrategyState
    {
        ShouldClosed,
        ShouldOpen,
        ShouldHalfOpen
    }
}