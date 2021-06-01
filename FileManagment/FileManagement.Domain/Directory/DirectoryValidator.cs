using Infrastructures.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.Domain.Directory
{
    internal class DirectoryValidator : Validator<Directory>
    {
        public DirectoryValidator(Directory entity, IValidatorNotificationHandler notificationHandler) : base(entity, notificationHandler)
        {
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
