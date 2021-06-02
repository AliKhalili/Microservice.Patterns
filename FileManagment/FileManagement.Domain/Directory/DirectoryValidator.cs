using Infrastructures.Domain;
using System;
using System.Linq;

namespace FileManagement.Domain.Directory
{
    internal class DirectoryValidator : Validator<Directory>
    {
        public DirectoryValidator(Directory entity, IValidatorNotificationHandler notificationHandler) : base(entity, notificationHandler)
        {
        }

        private bool CheckOwnerUserId()
        {
            if (_entity.OwnerUserId == null)
            {
                _notificationHandler.HandleError($"{_entity.OwnerUserId} is null");
                return false;
            }

            return true;
        }

        private bool CheckName()
        {
            if (_entity.Name == null)
            {
                _notificationHandler.HandleError($"{_entity.Name} is null");
                return false;
            }

            return true;
        }

        private bool CheckId()
        {
            if (_entity.Id == null)
            {
                _notificationHandler.HandleError($"{_entity.Id} is null");
                return false;
            }
            return true;
        }

        private bool CheckDirectoryItems()
        {
            if (_entity.DirectoryItems.GroupBy(x => x.Id).Any(x => x.Count() > 1))
            {
                _notificationHandler.HandleError($"{_entity.DirectoryItems} have duplicate items");
                return false;
            }
            return true;
        }

        private bool CheckModifiedDateTime()
        {
            if (_entity.ModifiedDateTime != null && _entity.CreatedDateTime < _entity.ModifiedDateTime)
            {
                _notificationHandler.HandleError($"{_entity.ModifiedDateTime} is lower than {_entity.CreatedDateTime}");
                return false;
            }
            return true;
        }

        public override bool Validate()
        {
            var isValid = CheckOwnerUserId();
            isValid &= CheckName();
            isValid &= CheckId();
            isValid &= CheckDirectoryItems();
            isValid &= CheckModifiedDateTime();
            return isValid;
        }
    }
}
