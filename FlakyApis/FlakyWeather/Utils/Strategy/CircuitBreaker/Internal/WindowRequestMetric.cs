using System;
using System.Collections.Generic;

namespace FlakyApi.Utils.Strategy.CircuitBreaker.Internal
{
    internal class WindowRequestMetric
    {
        private readonly long _samplingDuration;
        private readonly long _windowDuration;
        private readonly Queue<RequestMetric> _windows;
        private RequestMetric _currentWindow;

        public WindowRequestMetric(TimeSpan samplingDuration, int numberOfWindows)
        {
            _samplingDuration = samplingDuration.Ticks;
            _windowDuration = _samplingDuration / numberOfWindows;
            _windows = new Queue<RequestMetric>(numberOfWindows + 1);
        }

        public void IncrementSuccess()
        {
            RefreshWindowsMetricPerRequest();
            _currentWindow.IncrementSucceed();
        }

        public void IncrementFailure()
        {
            RefreshWindowsMetricPerRequest();
            _currentWindow.IncrementFailure();
        }

        public (int SuccessesCount, int FailuresCount) GetMetrics()
        {
            RefreshWindowsMetricPerRequest();
            int successes = 0;
            int failures = 0;
            
            foreach (var window in _windows)
            {
                successes += window.NumberOfSucceed;
                failures += window.NumberOfFailure;
            }

            return (successes, failures);
        }

        public void Reset()
        {
            _currentWindow = null;
            _windows.Clear();
        }
        private void RefreshWindowsMetricPerRequest()
        {
            var now = Clock.Now.Ticks;
            if (_currentWindow == null || now - _currentWindow.StartAt >= _windowDuration)
            {
                _currentWindow = new RequestMetric(now);
                _windows.Enqueue(_currentWindow);
            }

            while (_windows.TryPeek(out var headWindow) && headWindow.StartAt >= _samplingDuration)
                _windows.Dequeue();
        }
    }
}