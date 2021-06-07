using System;

namespace BuildingBlock.Domain
{
    public class Identity
    {
        public static Guid NewId => Guid.NewGuid();
    }
}