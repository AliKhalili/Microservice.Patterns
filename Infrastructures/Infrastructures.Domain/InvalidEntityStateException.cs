using System;

namespace Infrastructures.Domain
{
    /// <summary>
    /// Exception thrown by <see cref="AggregateRoot{TId}.EnsureValidState"/> when the aggregate felt in invalid state
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