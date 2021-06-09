using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManagement.Infrastructure.Directory
{
    public class FileManagementEntryEntityTypeConfiguration : IEntityTypeConfiguration<DirectoryEntity>
    {
        public void Configure(EntityTypeBuilder<DirectoryEntity> builder)
        {
            builder.ToTable("Entry", "fm");
            builder.HasKey(c => c.Id);
        }
    }
}