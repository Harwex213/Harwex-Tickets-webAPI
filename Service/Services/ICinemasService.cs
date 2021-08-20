using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models.Cinema;

namespace Service.Services
{
    public interface ICinemasService
    {
        public Task<CreateCinemaResponseModel> AddAsync(CreateCinemaModel createCinemaModel);
        public Task DeleteAsync(long entityId);
        public Task UpdateAsync(UpdateCinemaModel updateCinemaModel);
        public CinemaResponseModel Get(long entityId);
        public IEnumerable<CinemaResponseModel> GetAll();
    }
}