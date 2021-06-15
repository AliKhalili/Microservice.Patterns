using FileManagement.Domain.Directory.ValueObjects;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory
{
    public class DirectoryItem : Entity<DirectoryItemId>
    {
        public DirectoryId? DirectoryId { get; private set; }
        private string _name;

        protected override void When(IInternalEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}