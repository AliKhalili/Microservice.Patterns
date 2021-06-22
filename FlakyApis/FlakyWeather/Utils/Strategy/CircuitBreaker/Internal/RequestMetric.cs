namespace FlakyApi.Utils.Strategy.CircuitBreaker.Internal
{
    public class RequestMetric
    {
        public RequestMetric(long startAt)
        {
            StartAt = startAt;
        }

        public int NumberOfFailure { get; private set; }
        public int NumberOfSucceed { get; private set; }
        public int TotalRequests => NumberOfFailure + NumberOfSucceed;
        public long StartAt { get; }

        public void IncrementSucceed()
        {
            NumberOfSucceed++;
        }

        public void IncrementFailure()
        {
            NumberOfFailure++;
        }
    }
}