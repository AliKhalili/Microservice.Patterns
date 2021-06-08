using System;

namespace BuildingBlocks.Application
{
    public class CorrelationIds
    {
        public static Guid NewId => Guid.NewGuid();
    }
}