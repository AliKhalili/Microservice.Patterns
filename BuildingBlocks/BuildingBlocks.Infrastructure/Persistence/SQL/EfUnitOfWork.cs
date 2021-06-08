using System;
using System.Collections.Generic;

namespace BuildingBlocks.Infrastructure.Persistence.SQL
{
    public class EfUnitOfWork : IUnitOfWork, IRepositoryProvider
    {
        private readonly EfDbContext _context;

        private readonly IRepositoryProvider _repositoryProvider;
        public EfUnitOfWork(EfDbContext context, IEnumerable<IRepository> repositories = null)
        {
            _context = context;
            _repositoryProvider = new RepositoryProvider(repositories);
        }


        public async void CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void RollbackAsync()
        {
            throw new System.NotImplementedException();
        }

        public void AddRepository<T>(T repository) where T : IRepository
        {
            _repositoryProvider.AddRepository(repository);
        }

        public bool TryAddRepository<T>(T repository) where T : IRepository
        {
            return _repositoryProvider.TryAddRepository(repository);
        }

        public T GetRepository<T>(Type type) where T : IRepository
        {
            return _repositoryProvider.GetRepository<T>(type);
        }

        public bool TryGetRepository<T>(out T repository) where T : IRepository
        {
            return _repositoryProvider.TryGetRepository(out repository);
        }
    }
}