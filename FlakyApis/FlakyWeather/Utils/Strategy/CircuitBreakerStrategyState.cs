namespace FlakyApi.Utils.Strategy
{
    public enum CircuitBreakerStrategyState
    {
        ShouldClosed,
        ShouldOpen,
        ShouldHalfOpen
    }
}