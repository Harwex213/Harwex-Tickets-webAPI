using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models.Cinema;
using Service.Models.Hall;

namespace Service.Services
{
    public interface ICinemasService
    {
        public Task<CreateCinemaResponseModel> AddAsync(CreateCinemaModel createCinemaModel);
        public Task<CreateHallResponseModel> AddHallAsync(CreateHallModel createHallModel);
        public Task DeleteAsync(long entityId);
        public Task DeleteHallAsync(long hallId);
        public Task UpdateAsync(UpdateCinemaModel updateCinemaModel);
        public Task UpdateHallAsync(UpdateHallModel updateHallModel);
        public CinemaResponseModel Get(long entityId);
        public IEnumerable<CinemaResponseModel> GetAll();
    }
}