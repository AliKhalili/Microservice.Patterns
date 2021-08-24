using System;
using System.Collections.Generic;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryItemId(Guid Value) : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}