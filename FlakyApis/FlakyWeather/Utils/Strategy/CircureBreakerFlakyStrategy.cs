using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlakyWeather.Utils.Strategy
{
    public class CircuitBreakerFlakyStrategy : IFlakyStrategy
    {
        private long _lastRequestExecutingTick;
        private int _totalRequest;
        private int _totalFailedRequest;
        private CircuitBreakerStrategyState _circuitState;
        private readonly TimeSpan _shouldOpenTimeSpan;
        private readonly TimeSpan _shouldHalfOpenTimeSpan;
        private readonly TimeSpan _shouldCloseTimeSpan;
        private readonly double _shouldOpenFailureRatio;
        private readonly double _shouldHalfOpenFailureRatio;
        private readonly object _lock = new object();
        public CircuitBreakerFlakyStrategy(TimeSpan shouldOpenTimeSpan, TimeSpan shouldHalfOpenTimeSpan, TimeSpan shouldCloseTimeSpan)
        {
            _shouldOpenTimeSpan = shouldOpenTimeSpan;
            _shouldHalfOpenTimeSpan = shouldHalfOpenTimeSpan;
            _shouldCloseTimeSpan = shouldCloseTimeSpan;
            _circuitState = CircuitBreakerStrategyState.ShouldClosed;
            _shouldOpenFailureRatio = 4.0 / 5.0;
            _shouldHalfOpenFailureRatio = 1.0 / 5.0;
        }

        public Task<TResult> Execute<TResult>(Func<IDictionary<string, object>, Task<TResult>> func, IDictionary<string, object> parameters)
        {
            OnRequestExecuting();
            return func(parameters);
        }


        private void OnRequestExecuting()
        {
            lock (_lock)
            {
                _totalRequest++;
                _lastRequestExecutingTick = DateTime.UtcNow.Ticks;
                var durationOfChangeState = DateTime.UtcNow.Ticks - _lastRequestExecutingTick;
                switch (_circuitState)
                {
                    case CircuitBreakerStrategyState.ShouldClosed:
                        if (durationOfChangeState > _shouldCloseTimeSpan.Ticks)
                            _circuitState = CircuitBreakerStrategyState.ShouldHalfOpen;
                        break;
                    case CircuitBreakerStrategyState.ShouldHalfOpen:
                        if (durationOfChangeState > _shouldHalfOpenTimeSpan.Ticks)
                            _circuitState = CircuitBreakerStrategyState.ShouldOpen;
                        if ((double) _totalFailedRequest / _totalRequest < _shouldHalfOpenFailureRatio)
                        {
                            _totalFailedRequest++;
                            throw new FlakyServiceHalfOpenException();
                        }
                        Reset();
                        _circuitState = CircuitBreakerStrategyState.ShouldOpen;
                        break;
                    case CircuitBreakerStrategyState.ShouldOpen:
                        if (durationOfChangeState > _shouldOpenTimeSpan.Ticks)
                            _circuitState = CircuitBreakerStrategyState.ShouldOpen;
                        if ((double)_totalFailedRequest / _totalRequest < _shouldHalfOpenFailureRatio)
                        {
                            _totalFailedRequest++;
                            throw new FlakyServiceHalfOpenException();
                        }
                        Reset();
                        _circuitState = CircuitBreakerStrategyState.ShouldOpen;
                        break;
                    default:
                        throw new InvalidOperationException("Unhandled CircuitState.");
                }
            }
        }
        private void Reset() { }
    }

    public class FlakyServiceHalfOpenException : Exception
    {

    }

    public class FlakyServiceOpenException : Exception
    {

    }
}