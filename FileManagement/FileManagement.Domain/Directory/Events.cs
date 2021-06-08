using FileManagement.Domain.Directory.ValueObjects;
using FileManagement.Domain.User;
using BuildingBlocks.Domain;

namespace FileManagement.Domain.Directory
{
    public record DirectoryCreated(DirectoryId Id,DirectoryName Name, UserId OwnerUserId, DirectoryId ParentDirectoryId) : IInternalEvent;
    public record DirectoryNewItemAdded(DirectoryItem NewItem) : IInternalEvent;
    public record DirectoryNewItemDeleted(DirectoryItem DeletedItem) : IInternalEvent;
    public record DirectoryRenamed(DirectoryName NewName) : IInternalEvent;
    public record DirectoryMoved(DirectoryId NewParentId) : IInternalEvent;

}