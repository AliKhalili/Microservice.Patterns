using System;
using System.Collections.Generic;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.User
{
    public record UserId(Guid Value) : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}