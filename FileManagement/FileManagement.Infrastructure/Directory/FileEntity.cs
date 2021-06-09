namespace FileManagement.Infrastructure.Directory
{
    public class FileEntity : FileManagementEntryEntity
    {
        public string ContentType { get; set; }
        public string OriginalName { get; set; }

    }
}