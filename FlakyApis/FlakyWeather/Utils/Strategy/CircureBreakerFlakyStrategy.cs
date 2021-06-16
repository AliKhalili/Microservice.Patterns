using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlakyApi.Utils.Strategy
{
    public class CircuitBreakerFlakyStrategy : IFlakyStrategy
    {
        private long _lastChangeStateTime;
        private CircuitBreakerStrategyState _circuitState;
        private readonly TimeSpan _durationOfBreak;
        private readonly object _lock = new object();
        public CircuitBreakerFlakyStrategy(TimeSpan durationOfBreak)
        {
            _durationOfBreak = durationOfBreak;
            Reset();
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
                var timePassedFromLastChangeState = DateNow - _lastChangeStateTime;
                if (timePassedFromLastChangeState > _durationOfBreak.Ticks)
                {
                    switch (_circuitState)
                    {
                        case CircuitBreakerStrategyState.ShouldClosed:
                            _circuitState = CircuitBreakerStrategyState.ShouldOpen;
                            break;
                        case CircuitBreakerStrategyState.ShouldHalfOpen:
                            _circuitState = new Random(DateTime.UtcNow.Millisecond).NextDouble() > 0.5 ? 
                                CircuitBreakerStrategyState.ShouldClosed :
                                CircuitBreakerStrategyState.ShouldOpen;
                            break;
                        case CircuitBreakerStrategyState.ShouldOpen:
                            _circuitState = CircuitBreakerStrategyState.ShouldHalfOpen;
                            break;
                        default:
                            throw new InvalidOperationException("Unhandled CircuitState.");
                    }
                    _lastChangeStateTime = DateNow;
                }

                if (_circuitState == CircuitBreakerStrategyState.ShouldHalfOpen)
                    if (new Random(DateTime.UtcNow.Millisecond).NextDouble() > 0.5)
                        throw new FlakyServiceHalfOpenException();

                if (_circuitState == CircuitBreakerStrategyState.ShouldOpen)
                    throw new FlakyServiceOpenException();
            }
        }

        private void Reset()
        {
            lock (_lock)
            {
                _circuitState = CircuitBreakerStrategyState.ShouldClosed;
                _lastChangeStateTime = DateNow;
            }
        }

        private long DateNow => DateTime.UtcNow.Ticks;
    }

    public class FlakyServiceHalfOpenException : Exception
    {

    }

    public class FlakyServiceOpenException : Exception
    {

    }
}