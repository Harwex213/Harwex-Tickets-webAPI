using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Service.Services.Abstract;

namespace Service.Services
{
    public class CinemaMoviesService : CrudService<CinemaMovie>, ICinemaMoviesService
    {
        public CinemaMoviesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}