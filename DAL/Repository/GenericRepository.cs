using DAL.Repository.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _set;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            _set.Add(entity);
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _set.Find(id);
            _set.Remove(entity);
            await SaveAsync();
        }

        public TEntity Get(int id)
        {
            return _set.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = _set.AsQueryable();
            foreach (var item in entities)
            {
                _set.Include(x => item);
            }

            return _set.AsEnumerable();
        }
        public bool Exists(TEntity entity)
        {
            return _set.Local.Any(e => e == entity);
        }

        public void Detach(TEntity entity)
        {
            ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Detached);
        }

        public void Attach(TEntity entity)
        {
            _set.Attach(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }
    }
}
