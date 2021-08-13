using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Service
{
    public abstract class CrudService<TEntity> : ICrudService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        protected CrudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task Add(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Add(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task Delete(long entityId)
        {
            var entity = _unitOfWork.Repository<TEntity>().Find(entityId);
            _unitOfWork.Repository<TEntity>().Delete(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _unitOfWork.Repository<TEntity>().Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual TEntity Get(int entityId)
        {
            return _unitOfWork.Repository<TEntity>().Find(entityId);
        }

        public virtual IList<TEntity> GetAll()
        {
            return _unitOfWork.Repository<TEntity>().GetAll();
        }
    }
}