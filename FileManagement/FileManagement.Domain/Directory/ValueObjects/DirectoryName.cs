using System;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryName : ValueObject
    {
        private readonly int _maxDirectoryName = 100;

        public DirectoryName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(DirectoryName));

            if (value.Length > _maxDirectoryName)
                throw new MaxLengthValidationException(nameof(DirectoryName), _maxDirectoryName);

            Value = value;
        }

        public string Value { get; }
    }
}