using System;
using BuildingBlock.Domain;
using FileManagement.Domain.User;
using Xunit;

namespace FileManagement.Tests
{
    public class UserValueObjectsTest
    {
        [Fact]
        public void Should_CreateDirectoryId_When_Instantiate_DirectoryId()
        {
            var userId = new UserId(Identity.NewId);
            Assert.IsType<UserId>(userId);
            Assert.True(Guid.TryParse(userId.Value.ToString(), out Guid temp));
        }
    }
}