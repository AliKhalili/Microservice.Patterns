using System;
using System.Collections.Generic;
using System.Linq;
using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using Infrastructures.Domain;

namespace FileManagement.Domain.Directory
{
    public class Directory : AggregateRoot<DirectoryId>
    {
        private DirectoryId? _parentId;
        private UserId _ownerUserId;
        private IList<DirectoryItem> _directoryItems;
        private DirectoryName _name;
        private DateTime _createdDateTime;
        private DateTime? _modifiedDateTime;

        #region When Methods
        protected override void When(IInternalEvent @event) => When((dynamic)@event);
        private void When(DirectoryCreated @event)
        {
            Id = new DirectoryId(Identity.NewId);
            _name = @event.Name;
            _createdDateTime = Clock.Now;
            _parentId = @event.ParentDirectoryId;
            _directoryItems = new List<DirectoryItem>();
        }

        private void When(NewItemAdded @event)
        {
            _modifiedDateTime = Clock.Now;
            _directoryItems.Add(@event.NewItem);
        }
        #endregion


        protected override void EnsureValidState()
        {
            bool valid = Id != null && _ownerUserId != null && _name != null;
            valid &= !_directoryItems.GroupBy(x => x.Id).Any(x => x.Count() > 1);
            valid &= _createdDateTime < _modifiedDateTime;
            if (!valid)
                throw new InvalidEntityStateException(this);

        }

        public Directory(
            DirectoryName name,
            UserId ownerUserId,
            DirectoryId? parentId)
        {
            Apply(new DirectoryCreated(name, ownerUserId, parentId));
        }

        public void AddNewItem(DirectoryItem directoryItem)
        {
            Apply(new NewItemAdded(directoryItem));
        }

    }
}