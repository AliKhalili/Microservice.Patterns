using FileManagement.Domain.Directory;
using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using Infrastructures.Domain;
using Xunit;

namespace FileManagement.Tests
{
    public class DirectoryTest
    {
        [Fact]
        public void Should_CreateAggregate_When_CreateNewInstance()
        {
            var directory = new Directory(new DirectoryName("test_name"), new UserId(Identity.NewId), null);
            Assert.IsType<Directory>(directory);
        }

        [Fact]
        public void Should_ThrowMaxLengthValidationException_When_DirectoryNameExceedStringLength()
        {
            var invalidMaxLengthDirectoryName = "abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz";
            Assert.Throws<MaxLengthValidationException>(() =>
            {
                var directory = new Directory(new DirectoryName(invalidMaxLengthDirectoryName), new UserId(Identity.NewId), null);
            });
        }
    }
}