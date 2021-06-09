using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Persistence.SQL
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}