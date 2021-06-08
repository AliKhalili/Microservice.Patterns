using System;
using System.Linq;
using BuildingBlock.Domain;
using FileManagement.Domain.Directory;
using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using FileManagement.Tests.Utils;
using Xunit;

namespace FileManagement.Tests
{
    public class DirectoryTest
    {
        private readonly IValidatorNotificationHandler _validatorNotificationHandler;
        private readonly Directory _entity;

        public DirectoryTest()
        {
            _validatorNotificationHandler = new FakeValidationNotificationHandler();
            _entity = new Directory(_validatorNotificationHandler);
        }

        [Fact]
        public void Should_CreateDirectory_When_CreateNew()
        {
            var directory = _entity.CreateNew(new DirectoryName("test_name"), new UserId(Identity.NewId), null);
            Assert.IsType<Directory>(directory);
            Assert.True(Guid.TryParse(directory.Id.Value.ToString(), out Guid temp));
        }

        [Fact]
        public void Should_AddDirectoryCreatedDomainEvent_When_CreateNew()
        {
            var directory = _entity.CreateNew(new DirectoryName("test_name"), new UserId(Identity.NewId), null);
            var domainEvent = (DirectoryCreated)directory.GetChanges().Single();
            Assert.NotNull(domainEvent);
            Assert.Equal(domainEvent.Id, directory.Id);
        }
    }
}