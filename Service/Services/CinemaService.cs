using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Service
{
    public class CinemaService : CrudService<Cinema>, ICinemasService
    {
        public CinemaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}