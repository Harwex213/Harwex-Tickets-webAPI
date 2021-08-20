using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models.Cinema;

namespace Domain.Interfaces.Services
{
    public interface ICinemasService : ICrudService<Cinema>
    {
        public Task<CreateCinemaResponseModel> AddAsync(CreateCinemaModel createCinemaModel);
    }
}