using System.Threading.Tasks;

namespace FlakyApi.Implementation
{
    public class DefaultFlakyService : IFlakyService
    {
        private readonly bool[] _statusSample;
        private int _timeStep;
        private readonly int _totalStep;
        private readonly object _lock = new object();

        public DefaultFlakyService()
        {
            _totalStep = 100_000;
            _timeStep = 0;
            _statusSample = new FailureEvents(_totalStep).RetrieveSample();
        }
        public Task<bool> RetrieveSystemStatus()
        {
            lock (_lock)
            {
                var currentSystemStatus = _statusSample[_timeStep % _totalStep];
                _timeStep++;
                return Task.FromResult(currentSystemStatus);
            }
        }
    }
}