using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Service.Exceptions;

namespace Service.Services.Abstract
{
    public abstract class CrudService<TEntity> : ICrudService<TEntity> where TEntity : class
    {
        protected IUnitOfWork UnitOfWork { get; }

        protected CrudService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Add(entity);
            await UnitOfWork.CommitAsync();
        }

        public virtual async Task DeleteAsync(long entityId)
        {
            var entity = UnitOfWork.Repository<TEntity>().Find(entityId);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            UnitOfWork.Repository<TEntity>().Delete(entity);
            await UnitOfWork.CommitAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Update(entity);
            await UnitOfWork.CommitAsync();
        }

        public virtual TEntity Get(long entityId)
        {
            return UnitOfWork.Repository<TEntity>().Find(entityId);
        }

        public virtual IList<TEntity> GetAll()
        {
            return UnitOfWork.Repository<TEntity>().GetAll();
        }
    }
}