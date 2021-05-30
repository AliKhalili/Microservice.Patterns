using System.Collections.Generic;
using System.Linq;

namespace Infrastructures.Domain
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler
        where TId : ValueObject<TId>
    {
        public TId Id { get; protected set; }

        protected abstract void When(IInternalEvent @event);

        private readonly List<IInternalEvent> _changes;

        protected AggregateRoot() => _changes = new List<IInternalEvent>();

        protected void Apply(IInternalEvent @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, IInternalEvent @event)
            => entity?.Handle(@event);

        void IInternalEventHandler.Handle(IInternalEvent @event) => When(@event);
    }
}