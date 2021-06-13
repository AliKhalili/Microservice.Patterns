using BuildingBlocks.Infrastructure.Persistence.SQL;
using FileManagement.Infrastructure.Directory;
using Microsoft.EntityFrameworkCore;

namespace FileManagement.Infrastructure
{
    public class FileManagementContext : EfDbContext
    {
        public FileManagementContext(DbContextOptions<FileManagementContext> options) : base(options)
        {
            var t = Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new FileManagementEntryEntityTypeConfiguration());
            builder.Entity<DirectoryEntity>(x => x.HasBaseType<FileManagementEntryEntity>());
            builder.Entity<FileEntity>(x => x.HasBaseType<FileManagementEntryEntity>());

        }

        public DbSet<FileManagementEntryEntity> Entries { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<DirectoryEntity> Directories { get; set; }

    }
}