using System;

namespace Infrastructures.Domain
{
    public abstract class Entity<TId> : IInternalEventHandler
        where TId : ValueObject<TId>
    {
        private readonly Action<IInternalEvent> _applier;

        public TId Id { get; protected set; }

        protected Entity(Action<IInternalEvent> applier) => _applier = applier;

        protected Entity() { }

        protected abstract void When(IInternalEvent @event);

        protected void Apply(IInternalEvent @event)
        {
            When(@event);
            _applier(@event);
        }

        void IInternalEventHandler.Handle(IInternalEvent @event) => When(@event);
    }
}