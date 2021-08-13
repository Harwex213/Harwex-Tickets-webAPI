using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext DbContext { get; }
        private Dictionary<string, object> Repositories { get; }

        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
            Repositories = new Dictionary<string, dynamic>();
        }

        public async Task<int> CommitAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
        
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            var typeName = type.Name;

            lock (Repositories)
            {
                if (Repositories.ContainsKey(typeName))
                {
                    return (IRepository<TEntity>) Repositories[typeName];
                }

                var repository = new Repository<TEntity>(DbContext);

                Repositories.Add(typeName, repository);
                return repository;
            }
        }
    }
}