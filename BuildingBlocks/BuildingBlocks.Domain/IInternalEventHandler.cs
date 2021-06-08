namespace BuildingBlocks.Domain
{
    public interface IInternalEventHandler
    {
        void Handle(IInternalEvent @event);
    }
}