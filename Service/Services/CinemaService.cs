using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Service.Services.Abstract;

namespace Service.Services
{
    public class CinemaService : CrudService<Cinema>, ICinemasService
    {
        public CinemaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}