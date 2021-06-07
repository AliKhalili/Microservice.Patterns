using System.Collections.Generic;
using System.Linq;

namespace BuildingBlock.Domain
{
    public abstract class AggregateRoot<TId> : AggregateRoot
        where TId : ValueObject
    {
        public TId Id { get; protected set; }
    }
    public abstract class AggregateRoot : IInternalEventHandler
    {
        protected abstract void When(IInternalEvent @event);

        private readonly List<IInternalEvent> _changes;

        protected AggregateRoot() => _changes = new List<IInternalEvent>();

        protected void Apply(IInternalEvent @event)
        {
            When(@event);
            Validate();
            _changes.Add(@event);
        }

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void Validate();

        protected void ApplyToEntity(IInternalEventHandler entity, IInternalEvent @event)
            => entity?.Handle(@event);

        void IInternalEventHandler.Handle(IInternalEvent @event) => When(@event);
    }
}