using System;

namespace BuildingBlocks.Domain
{
    public class Identity
    {
        public static Guid NewId => Guid.NewGuid();
    }
}