using Infrastructures.Domain;

namespace FileManagement.Domain.Directory.ValueObjects
{
    public record DirectoryName : ValueObject
    {
        private readonly int _maxDirectoryName = 100;
        
        public DirectoryName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new MaxLengthValidationException(nameof(DirectoryName), _maxDirectoryName);

            Value = value;
        }

        public string Value { get; }
    }
}