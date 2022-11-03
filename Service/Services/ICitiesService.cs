using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models.Cinema;
using Service.Models.City;

namespace Service.Services
{
    public interface ICitiesService
    {
        public Task<CreateCityResponseModel> AddAsync(CreateCityModel createCityModel);
        public Task DeleteAsync(long entityId);
        public Task UpdateAsync(UpdateCityModel updateCityModel);
        public CityResponseModel Get(long entityId);
        public IEnumerable<CityResponseModel> GetAll();
        public IEnumerable<CityResponseModel> GetAllByName(string name);
    }
}