using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlakyApi.Implementations
{
    public class ExponentialFailureEventsStrategy : IFlakyStrategy
    {
        private readonly ILogger<ExponentialFailureEventsStrategy> _logger;
        private readonly int _timeStepInterval;
        private readonly double _lambda;
        private int _timeStep;
        private readonly object _lock = new object();

        public ExponentialFailureEventsStrategy(ILogger<ExponentialFailureEventsStrategy> logger, IOptions<FlakyStrategyOptions> options)
        {
            _logger = logger;
            _timeStepInterval = options.Value.TimeStepInterval;
            _lambda = 1 / (double)options.Value.FirstEventOccurrenceTimeStep / 60 * _timeStepInterval;
            _timeStep = 0;
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
                var failureEventProbability = StatisticalUtils.ExponentialDistributionCdfFunc(_lambda, _timeStep);
                var isSystemUp = StatisticalUtils.NonUniformRandomChoice(new[] { 1 - failureEventProbability, failureEventProbability });
                _timeStep++;
                if (_timeStep == _timeStepInterval)
                    _timeStep = 0;
                if (isSystemUp == 1)
                    throw new ServiceCurrentlyUnavailableException();
            }
        }
    }
}