using System;
using System.Linq;

namespace FlakyApi.Implementations
{
    public static class StatisticalUtils
    {
        /// <summary>
        /// implementation of Cumulative distribution function fot Exponential Distribution
        /// <para><see><cref>https://en.wikipedia.org/wiki/Exponential_distribution</cref></see></para>
        /// </summary>
        public static double ExponentialDistributionCdfFunc(double lambda, int timeStep) => 1 - Math.Exp(-lambda * timeStep);

        /// <summary>
        /// this method is borrowed from <see><cref>https://stackoverflow.com/a/43345968/426983</cref></see>
        /// </summary>
        public static int NonUniformRandomChoice(double[] entriesProbabilities)
        {
            double sum = 0;
            // first change shape of your distribution probability array
            // we need it to be cumulative, that is:
            // if you have [0.1, 0.2, 0.3, 0.4] 
            // we need     [0.1, 0.3, 0.6, 1  ] instead
            var cumulative = entriesProbabilities.Select(c =>
            {
                var result = c + sum;
                sum += c;
                return result;
            }).ToList();

            // now generate random double. It will always be in range from 0 to 1
            var random = Random.NextDouble();
            // now find first index in our cumulative array that is greater or equal generated random value
            var idx = cumulative.BinarySearch(random);
            // if exact match is not found, List.BinarySearch will return index of the first items greater than passed value, but in specific form (negative)
            // we need to apply ~ to this negative value to get real index
            if (idx < 0)
                idx = ~idx;
            if (idx > cumulative.Count - 1)
                idx = cumulative.Count - 1; // very rare case when probabilities do not sum to 1 because of double precision issues (so sum is 0.999943 and so on)
            return idx;
        }

        private static readonly Random Random = new();
    }
}