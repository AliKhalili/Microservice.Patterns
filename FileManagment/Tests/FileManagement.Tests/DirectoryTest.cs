using FileManagement.Domain.Directory;
using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using FileManagement.Tests.Utils;
using Infrastructures.Domain;
using Xunit;

namespace FileManagement.Tests
{
    public class DirectoryTest
    {
        private readonly IValidatorNotificationHandler _validatorNotificationHandler;
        private readonly Directory _entity;

        public DirectoryTest()
        {
            _validatorNotificationHandler= new FakeValidationNotificationHandler();
            _entity = new Directory(_validatorNotificationHandler);
        }
        [Fact]
        public void Should_CreateAggregate_When_CreateNewInstance()
        {
            //setup
            var directory = _entity.CreateNew(new DirectoryName("test_name"), new UserId(Identity.NewId), null);
            Assert.IsType<Directory>(directory);
        }

        [Fact]
        public void Should_ThrowMaxLengthValidationException_When_DirectoryNameExceedStringLength()
        {
            var invalidMaxLengthDirectoryName = "abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz_abcdefghijklmnopqxyz";
            Assert.Throws<MaxLengthValidationException>(() =>
            {
                _entity.CreateNew(new DirectoryName(invalidMaxLengthDirectoryName), new UserId(Identity.NewId), null);
            });
        }
    }
}