namespace Infrastructures.Domain
{
    public interface IInternalEventHandler
    {
        void Handle(IInternalEvent @event);
    }
}