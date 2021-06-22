using System;
using FlakyApi.Utils;

namespace FlakyApi.Implementation
{
    /// <summary>
    /// Generate sample of system status(up/down) based on exponential distribution in each time step.
    /// <para>visit <seealso cref="https://en.wikipedia.org/wiki/Exponential_distribution"/> for get more information</para>
    /// </summary>
    internal class FailureEvents
    {
        private readonly int _numberOfEvents;
        private readonly double _failureThreshold;

        /// <summary>
        /// Initialized new instance of <see cref="FailureEvents"/>
        /// </summary>
        /// <param name="numberOfEvents">Number of consecutive events</param>
        /// <param name="failureThreshold">Set rate parameter in exponential distribution, 0.3 is the default value.</param>
        public FailureEvents(int numberOfEvents, double failureThreshold=0.3)
        {
            _numberOfEvents = numberOfEvents;
            _failureThreshold = failureThreshold;
        }

        /// <summary>
        /// Generate new sample of consecutive events that for each time step specify the current status of system.
        /// </summary>
        /// <returns>new sample of consecutive events which assign system status to each time step</returns>
        /// <example>
        /// This sample shows how this method retrieve new sample of consecutive events which assign system status to each time step.
        /// <code>
        /// var sample = new FailureEvents(numberOfEvents:10).RetrieveSample()
        /// # sample : [True, True, False, True, False, False, True, True, True, True]
        /// # time step 1 : system is up
        /// # time step 2 : system is up
        /// # time step 3 : system is down
        /// # and so on
        /// </code>
        /// </example>
        public bool[] RetrieveSample()
        {
            var samples = new bool[_numberOfEvents];
            for (int index = 0; index < _numberOfEvents; index++)
            {
                var nextRandomNumber = new Random(Clock.Now.Millisecond).NextDouble();
                var sample = ExponentialDistributionCumulativeDensityFunction(_failureThreshold, nextRandomNumber);
                samples[index] = sample <= 1;
            }
            return samples;
        }

        private double ExponentialDistributionCumulativeDensityFunction(double lambda, double x)
        {
            return 1 - lambda * Math.Exp(-lambda * x);
        }
    }
}