using System;
using System.Collections.Generic;
using System.Linq;
using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory
{
    public class Directory : AggregateRoot<DirectoryId>
    {
        public DirectoryId? ParentId { get; private set; }
        public UserId OwnerUserId { get; private set; }
        public IList<DirectoryItem> DirectoryItems { get; private set; }
        public DirectoryName Name { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime? ModifiedDateTime { get; private set; }

        private readonly DirectoryValidator _validator;

        public Directory(IValidatorNotificationHandler notificationHandler)
        {
            _validator = new DirectoryValidator(this, notificationHandler);
        }

        #region When Methods
        protected override void When(IInternalEvent @event) => When((dynamic)@event);
        private void When(DirectoryCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            CreatedDateTime = Clock.Now;
            OwnerUserId = @event.OwnerUserId;
            ParentId = @event.ParentDirectoryId;
            DirectoryItems = new List<DirectoryItem>();
        }

        private void When(DirectoryNewItemAdded @event)
        {
            ModifiedDateTime = Clock.Now;
            DirectoryItems.Add(@event.NewItem);
        }

        private void When(DirectoryRenamed @event)
        {
            ModifiedDateTime = Clock.Now;
            Name = @event.NewName;
        }
        #endregion


        protected override void Validate()
        {
            if (!_validator.Validate())
                throw new InvalidEntityStateException(this);
        }

        public Directory CreateNew(
            DirectoryName name,
            UserId ownerUserId,
            DirectoryId? parentId)
        {
            Apply(new DirectoryCreated(new DirectoryId(Identity.NewId),name, ownerUserId, parentId));
            return this;
        }

        public void AddNewItem(DirectoryItem directoryItem)
        {
            Apply(new DirectoryNewItemAdded(directoryItem));
        }

        public void DeleteItem(DirectoryItem directoryItem)
        {
            Apply(new DirectoryNewItemDeleted(directoryItem));
        }
        public void Rename(DirectoryName newName)
        {
            Apply(new DirectoryRenamed(newName));
        }

        public void Moved(DirectoryId newParentId)
        {
            Apply(new DirectoryMoved(newParentId));
        }

    }
}