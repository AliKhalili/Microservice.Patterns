using System;

namespace BuildingBlocks.Infrastructure.Persistence
{
    internal interface IRepositoryProvider
    {
        void AddRepository<T>(T repository) where T : IRepository;
        bool TryAddRepository<T>(T repository) where T : IRepository;
        T GetRepository<T>(Type type) where T : IRepository;
        bool TryGetRepository<T>(out T repository) where T : IRepository;
    }
}