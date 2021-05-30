using System;
using Infrastructures.Domain;

namespace FileManagement.Domain.Directory
{
    public class DirectoryId : ValueObject<DirectoryId>
    {
        public Guid Value { get; internal set; }

        protected DirectoryId() { }

        public DirectoryId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Directory id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(DirectoryId self) => self.Value;

        public static implicit operator DirectoryId(string value)
            => new(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}