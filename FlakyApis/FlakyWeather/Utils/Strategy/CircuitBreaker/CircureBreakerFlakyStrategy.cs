using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlakyApi.Implementations;
using FlakyApi.Utils.Strategy.CircuitBreaker.Internal;

namespace FlakyApi.Utils.Strategy.CircuitBreaker
{
    public class CircuitBreakerFlakyStrategy : IFlakyStrategy
    {
        private const short NumberOfWindows = 10;

        private long _lastChangeStateTime;
        private CircuitBreakerStrategyState _circuitState;
        private readonly float _failureThreshold;
        private readonly TimeSpan _samplingDuration;
        private readonly TimeSpan _durationOfBreak;
        private readonly WindowRequestMetric _metric;
        private readonly bool[] _systemStatusPerTimeStep;
        private readonly object _lock = new object();

        public CircuitBreakerFlakyStrategy(float failureThreshold, TimeSpan samplingDuration, TimeSpan durationOfBreak)
        {
            if (failureThreshold <= 0)
                throw new ArgumentOutOfRangeException(nameof(failureThreshold), "The given value must be greater than zero.");
            if (failureThreshold > 1)
                throw new ArgumentOutOfRangeException(nameof(failureThreshold), "The given value must be less than or equal to one.");
            if (durationOfBreak < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(durationOfBreak), "The given value must be greater than zero");

            _failureThreshold = failureThreshold;
            _samplingDuration = samplingDuration;
            _durationOfBreak = durationOfBreak;
            _metric = new WindowRequestMetric(samplingDuration, NumberOfWindows);
            _systemStatusPerTimeStep = new FailureEvents(100_000, failureThreshold).RetrieveSample();
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
                var now = Clock.Now.Ticks;
                var timePassedFromLastChangeState = now - _lastChangeStateTime;
                var isSystemUp = _systemStatusPerTimeStep[now % _systemStatusPerTimeStep.Length];
                if (now - _lastChangeStateTime > _durationOfBreak.Ticks)
                    OnTimeout();

                if (_circuitState == CircuitBreakerStrategyState.ShouldHalfOpen)
                    if (new Random(DateTime.UtcNow.Millisecond).NextDouble() > 0.5)
                        throw new FlakyServiceHalfOpenException();

                if (_circuitState == CircuitBreakerStrategyState.ShouldOpen)
                    throw new FlakyServiceOpenException();
            }
        }

        private void OnTimeout()
        {
            if (_circuitState == CircuitBreakerStrategyState.ShouldOpen)
                _circuitState = CircuitBreakerStrategyState.ShouldClosed;
        }

        private void OnBreak()
        {

        }
        private void Reset()
        {
            lock (_lock)
            {
                _circuitState = CircuitBreakerStrategyState.ShouldClosed;
                _lastChangeStateTime = Clock.Now.Ticks;
            }
        }
    }

    public class FlakyServiceHalfOpenException : Exception
    {

    }

    public class FlakyServiceOpenException : Exception
    {

    }
}