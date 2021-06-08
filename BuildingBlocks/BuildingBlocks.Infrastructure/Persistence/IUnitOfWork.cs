namespace BuildingBlocks.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        void CommitAsync();
        void RollbackAsync();
    }
}