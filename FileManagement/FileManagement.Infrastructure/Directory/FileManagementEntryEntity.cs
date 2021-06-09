using System;

namespace FileManagement.Infrastructure.Directory
{
    /// <summary>
    /// represent a table in sql server which implement TPH inheritance(table-per-hierarchy) for File Management Module.
    /// <br></br>TPH inheritance pattern generally deliver better performance in the entity framework than TPT(table-per-type) inheritance patterns, because TPT patterns can result in complex join quires.
    /// </summary>
    public class FileManagementEntryEntity
    {
        public Guid Id { get; set; }
        public Guid OwnerUserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public FileManagementEntryType EntryType { get; set; }

        public Guid? ParentDirectoryId { get; set; }
        public FileManagementEntryEntity ParentDirectory { get; set; }
    }
}