using System;
using Infrastructures.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryId(Guid Value) : ValueObject;
}