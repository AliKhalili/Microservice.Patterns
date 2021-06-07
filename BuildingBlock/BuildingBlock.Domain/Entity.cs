using System;

namespace BuildingBlock.Domain
{
    public abstract class Entity<TId> : Entity
        where TId : ValueObject
    {
        protected Entity(Action<IInternalEvent> applier) : base(applier)
        {

        }
        protected Entity() { }

        public TId Id { get; protected set; }
    }
    public abstract class Entity : IInternalEventHandler
    {
        private readonly Action<IInternalEvent> _applier;

        protected Entity() { }
        protected Entity(Action<IInternalEvent> applier) => _applier = applier;

        protected abstract void When(IInternalEvent @event);

        protected void Apply(IInternalEvent @event)
        {
            When(@event);
            _applier(@event);
        }

        void IInternalEventHandler.Handle(IInternalEvent @event) => When(@event);
    }
}