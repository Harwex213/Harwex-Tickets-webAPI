using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICrudService<TEntity>
    {
        public Task AddAsync(TEntity entity);
        public Task DeleteAsync(long entityId);
        public Task UpdateAsync(TEntity entity);
        public TEntity Get(long entityId);
        public IList<TEntity> GetAll();
    }
}