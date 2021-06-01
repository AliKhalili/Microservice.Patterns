using FileManagement.Domain.Directory.ValueObjects;
using Infrastructures.Domain;

namespace FileManagement.Domain.Directory
{
    public class DirectoryItem: Entity<DirectoryItemId>
    {
        private DirectoryId _directoryId;
        private string _name;

        protected override void When(IInternalEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}