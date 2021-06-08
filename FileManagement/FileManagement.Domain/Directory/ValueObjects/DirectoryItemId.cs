using System;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryItemId(Guid Value) : ValueObject
    {

    }
}