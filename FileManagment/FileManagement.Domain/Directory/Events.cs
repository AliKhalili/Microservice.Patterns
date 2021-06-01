using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using Infrastructures.Domain;

namespace FileManagement.Domain.Directory
{
    public record DirectoryCreated(DirectoryName Name, UserId OwnerUserId, DirectoryId ParentDirectoryId) : IInternalEvent;

    public record NewItemAdded(DirectoryItem NewItem) : IInternalEvent;

}