using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICrudService<TEntity>
    {
        public Task Add(TEntity entity);
        public Task Delete(long entityId);
        public Task Update(TEntity entity);
        public TEntity Get(int entityId);
        public IList<TEntity> GetAll();
    }
}