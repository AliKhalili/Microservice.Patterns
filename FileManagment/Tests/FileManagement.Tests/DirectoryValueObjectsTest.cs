using System;
using BuildingBlock.Domain;
using FileManagement.Domain.Directory.ValueObjects;
using Xunit;

namespace FileManagement.Tests
{
    public class DirectoryValueObjectsTest
    {
        [Fact]
        public void Should_CreateDirectoryId_When_Instantiate_DirectoryId()
        {
            var directoryId = new DirectoryId(Identity.NewId);
            Assert.IsType<DirectoryId>(directoryId);
            Assert.True(Guid.TryParse(directoryId.Value.ToString(), out Guid temp));
        }

        [Fact]
        public void Should_CreateDirectoryName_When_Instantiate_DirectoryName()
        {
            var validDirectoryName = "test directory name";
            var directoryName = new DirectoryName(validDirectoryName);
            Assert.IsType<DirectoryName>(directoryName);
            Assert.Equal(directoryName.Value, validDirectoryName);
        }

        [Fact]
        public void Should_ThrowArgumentNullException_When_DirectoryNameIsNullOrWhiteSpace()
        {
            var nullDirectoryName = string.Empty;
            Assert.Throws<ArgumentNullException>(() =>
            {
                var directoryName = new DirectoryName(nullDirectoryName);
            });
        }

        [Fact]
        public void Should_ThrowMaxLengthValidationException_When_DirectoryNameExceedStringLength()
        {
            var invalidMaxLengthDirectoryName = "abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz";
            Assert.Throws<MaxLengthValidationException>(() =>
            {
                var directoryName = new DirectoryName(invalidMaxLengthDirectoryName);
            });
        }


        [Fact]
        public void Should_CreateDirectoryItemId_When_Instantiate_DirectoryItemId()
        {
            var directoryItemId = new DirectoryItemId(Identity.NewId);
            Assert.IsType<DirectoryItemId>(directoryItemId);
            Assert.True(Guid.TryParse(directoryItemId.Value.ToString(), out Guid temp));
        }
    }
}