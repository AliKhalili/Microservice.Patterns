using System;

namespace FlakyApi.Implementations
{
    public class ServiceCurrentlyUnavailableException : Exception
    {
        public ServiceCurrentlyUnavailableException(int relativeTimeStep)
        {
            RelativeTimeStep = relativeTimeStep;
        }

        public int RelativeTimeStep { get; }
    }
}