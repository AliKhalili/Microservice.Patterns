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
            Id = new DirectoryId(Identity.NewId);
            Name = @event.Name;
            CreatedDateTime = Clock.Now;
            OwnerUserId = @event.OwnerUserId;
            ParentId = @event.ParentDirectoryId;
            DirectoryItems = new List<DirectoryItem>();
        }

        private void When(NewItemAdded @event)
        {
            ModifiedDateTime = Clock.Now;
            DirectoryItems.Add(@event.NewItem);
        }
        #endregion


        protected override void Validate()
        {
            if (_validator.Validate())
                throw new InvalidEntityStateException(this);
        }

        public Directory CreateNew(
            DirectoryName name,
            UserId ownerUserId,
            DirectoryId? parentId)
        {
            Id = new DirectoryId(Identity.NewId);
            Name = name;
            OwnerUserId = ownerUserId;
            ParentId = parentId;
            CreatedDateTime = Clock.Now;

            Apply(new DirectoryCreated(name, ownerUserId, parentId));
            return this;
        }

        public void AddNewItem(DirectoryItem directoryItem)
        {
            Apply(new NewItemAdded(directoryItem));
        }

    }
}