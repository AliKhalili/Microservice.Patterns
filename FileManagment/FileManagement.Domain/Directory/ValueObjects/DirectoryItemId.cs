using System;
using Infrastructures.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryItemId(Guid Value) : ValueObject
    {

    }
}