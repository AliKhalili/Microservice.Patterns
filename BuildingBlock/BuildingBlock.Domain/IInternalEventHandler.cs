namespace BuildingBlock.Domain
{
    public interface IInternalEventHandler
    {
        void Handle(IInternalEvent @event);
    }
}