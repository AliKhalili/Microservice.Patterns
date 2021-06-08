using System;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryId(Guid Value) : ValueObject;
}