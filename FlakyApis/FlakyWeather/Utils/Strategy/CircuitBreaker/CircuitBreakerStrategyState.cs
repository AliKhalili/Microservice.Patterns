namespace FlakyApi.Utils.Strategy.CircuitBreaker
{
    public enum CircuitBreakerStrategyState
    {
        ShouldClosed,
        ShouldOpen,
        ShouldHalfOpen
    }
}