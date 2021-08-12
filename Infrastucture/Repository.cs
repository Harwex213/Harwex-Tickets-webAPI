using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private DbSet<T> _dbSet;

        private DbSet<T> DbSet => _dbSet ??= _dbContext.Set<T>();

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
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

        public IQueryable<T> List(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }
    }
}