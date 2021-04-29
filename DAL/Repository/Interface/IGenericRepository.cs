using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        bool Exists(TEntity entity);
        void Detach(TEntity entity);
        void Attach(TEntity entity);
    }
}
