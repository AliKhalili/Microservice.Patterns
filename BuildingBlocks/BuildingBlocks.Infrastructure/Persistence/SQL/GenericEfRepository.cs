using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Persistence.SQL
{
    public abstract class GenericEfRepository<T,TId> where  T:Entity<TId>
    {
        private readonly DbSet<T> _entities;

        public GenericEfRepository(DbSet<T> entities)
        {
            this._entities = entities;
        }

        protected IQueryable<T> Query()
        {
            return _entities.AsNoTracking();
        }

        public abstract T FindById(TId id);
        
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }
    }
}