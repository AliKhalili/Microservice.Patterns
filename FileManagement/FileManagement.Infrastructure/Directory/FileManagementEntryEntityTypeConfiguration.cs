using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManagement.Infrastructure.Directory
{
    public class FileManagementEntryEntityTypeConfiguration : IEntityTypeConfiguration<FileManagementEntryEntity>
    {
        public void Configure(EntityTypeBuilder<FileManagementEntryEntity> builder)
        {
            builder
                .ToTable("Entries", "fm")
                .HasDiscriminator<FileManagementEntryType>(nameof(FileManagementEntryEntity.EntryType))
                .HasValue<FileManagementEntryEntity>(FileManagementEntryType.Unknown)
                .HasValue<DirectoryEntity>(FileManagementEntryType.Directory)
                .HasValue<FileEntity>(FileManagementEntryType.File);
            builder
                .HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.ParentDirectoryId)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}