using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Find(params object[] keys);
        IList<T> GetAll();
        IQueryable<T> List(Expression<Func<T, bool>> expression);
    }
}