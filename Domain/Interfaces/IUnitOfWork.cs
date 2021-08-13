using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        IRepository<T> Repository<T>() where T : class;
    }
}