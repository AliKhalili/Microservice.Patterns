using System;

namespace BuildingBlock.Domain
{
    /// <summary>
    /// Exception thrown by <see cref="AggregateRoot.Validate"/> when the aggregate felt in invalid state
    /// </summary>
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string message)
            : base($"Entity {entity.GetType().Name} state change rejected, {message}")
        {
        }
        public InvalidEntityStateException(object entity)
            : base($"Entity {entity.GetType().Name} state change rejected")
        {
        }
    }
}