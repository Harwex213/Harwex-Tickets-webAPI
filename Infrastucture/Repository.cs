using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> DbSet => _dbSet ??= _dbContext.Set<T>();

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public IList<T> GetAll()
        {
            return DbSet.ToList();
        }
    }
}