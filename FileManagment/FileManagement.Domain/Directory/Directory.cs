using Infrastructures.Domain;

namespace FileManagement.Domain.Directory
{
    public class Directory : AggregateRoot<DirectoryId>
    {
        protected override void When(IInternalEvent @event)
        {
            throw new System.NotImplementedException();
        }

        protected override void EnsureValidState()
        {
            throw new System.NotImplementedException();
        }
    }
}