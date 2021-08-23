using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models.Movie;

namespace Service.Services
{
    public interface IMoviesService
    {
        public Task<CreateMovieResponseModel> AddAsync(CreateMovieModel createMovieModel);
        public Task DeleteAsync(long entityId);
        public Task UpdateAsync(UpdateMovieModel updateMovieModel);
        public MovieResponseModel Get(long entityId);
        public IEnumerable<MovieResponseModel> GetAll();
    }
}