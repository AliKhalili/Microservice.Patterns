using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingBlocks.Infrastructure.Persistence
{
    internal sealed class RepositoryProvider : IRepositoryProvider
    {
        private readonly IDictionary<Type, IRepository> _repositories;

        public RepositoryProvider(IEnumerable<IRepository> repositories)
        {
            _repositories = repositories?.ToDictionary(x => x.GetType(), x => x) ?? 
                            new Dictionary<Type, IRepository>();
        }

        public void AddRepository<T>(T repository) where T : IRepository
        {
            if (_repositories.ContainsKey(repository.GetType()))
                throw new ArgumentException($"a repository of type {repository.GetType()} have been added before");
            _repositories[repository.GetType()] = repository;
        }

        public bool TryAddRepository<T>(T repository) where T : IRepository
        {
            if (_repositories.ContainsKey(repository.GetType()))
                return false;

            _repositories[repository.GetType()] = repository;
            return true;
        }

        public T GetRepository<T>(Type type) where T : IRepository
        {
            if (!type.IsInstanceOfType(typeof(IRepository)))
                throw new NotSupportedException($"The {nameof(GetRepository)} does not support {type}");

            if (!_repositories.ContainsKey(type))
                throw new ArgumentException($"there is not any repository of type {type}, it should be add by {nameof(AddRepository)} method");

            return (T)_repositories[type];
        }

        public bool TryGetRepository<T>(out T repository) where T : IRepository
        {
            var type = typeof(T);
            repository = default;
            if (!type.IsInstanceOfType(typeof(IRepository)))
                throw new NotSupportedException($"The {nameof(TryGetRepository)} does not support {type}");
            if (!_repositories.ContainsKey(type))
                return false;

            repository = (T) _repositories[type];
            return true;
        }
    }
}