using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FlakyApi.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlakyApi.Implementations
{
    public class ExponentialFailureEventsStrategy : IFlakyStrategy
    {
        private static long _durationOfEachTimeStep = TimeSpan.FromSeconds(1).Ticks;

        private readonly ILogger<ExponentialFailureEventsStrategy> _logger;
        private readonly int _timeStepInterval;
        private readonly double _lambda;
        private int _timeStep;
        private long _startTimeTicks;
        private readonly object _lock = new();

        public ExponentialFailureEventsStrategy(ILogger<ExponentialFailureEventsStrategy> logger, IOptions<FlakyStrategyOptions> options)
        {
            _logger = logger;
            _timeStepInterval = options.Value.TimeStepInterval;
            _lambda = 1 / (double)options.Value.FirstEventOccurrenceTimeStep;
            _startTimeTicks = Clock.Now.Ticks;
            _timeStep = 0;
            _logger.LogInformation("reset of {0} is called", nameof(ExponentialFailureEventsStrategy));
        }

        public Task<TResult> Execute<TResult>(Func<IDictionary<string, object>, Task<TResult>> func, IDictionary<string, object> parameters)
        {
            OnRequestExecuting();
            return func(parameters);
        }

        public Task Reset()
        {
            _timeStep = 0;
            _startTimeTicks = Clock.Now.Ticks;
            _logger.LogInformation("strategy is rested");
            return Task.CompletedTask;
        }

        private void OnRequestExecuting()
        {
            lock (_lock)
            {
                var failureEventProbability = StatisticalUtils.ExponentialDistributionCdfFunc(_lambda, _timeStep);
                var isSystemDown = StatisticalUtils.NonUniformRandomChoice(new[] { 1 - failureEventProbability, failureEventProbability });

                _timeStep = CurrentTimeStep();
                if (CurrentTimeStep() >= _timeStepInterval)
                    Reset();

                if (isSystemDown == 1)
                    throw new ServiceCurrentlyUnavailableException(_timeStep);
            }
        }

        private int CurrentTimeStep()
        {
            var now = Clock.Now.Ticks;
            return (int)((now - _startTimeTicks) / _durationOfEachTimeStep);
        }
    }
}