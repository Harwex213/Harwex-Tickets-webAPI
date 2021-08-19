using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Service.Services.Abstract;

namespace Service.Services
{
    public class CityService: CrudService<City>, ICitiesService
    {
        public CityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}