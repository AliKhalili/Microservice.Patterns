using System;

namespace Infrastructures.Domain
{
    public class Identity
    {
        public static Guid NewId => Guid.NewGuid();
    }
}