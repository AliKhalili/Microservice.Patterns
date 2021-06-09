using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FileManagement.Infrastructure
{
    public static class FileManagementInfrastructureExtensions
    {
        public static void AddFileManagementInfrastructure(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<FileManagementContext>(options => options.UseSqlServer(connectionString));
        }
    }
}