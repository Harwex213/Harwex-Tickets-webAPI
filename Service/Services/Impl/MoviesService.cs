using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Service.Exceptions;
using Service.Models.Movie;

namespace Service.Services.Impl
{
    public class MoviesService : IMoviesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Movie> _movieRepository;

        public MoviesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _movieRepository = unitOfWork.Repository<Movie>();
        }

        public async Task<CreateMovieResponseModel> AddAsync(CreateMovieModel createMovieModel)
        {
            var movieEntity = _mapper.Map<Movie>(createMovieModel);
            
            _movieRepository.Add(movieEntity);
            await _unitOfWork.CommitAsync();
            
            return _mapper.Map<CreateMovieResponseModel>(movieEntity);
        }

        public async Task DeleteAsync(long entityId)
        {
            var movieEntity = _movieRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(movieEntity);
            
            _movieRepository.Delete(movieEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateMovieModel updateMovieModel)
        {
            var movieEntity = _mapper.Map<Movie>(updateMovieModel);
            
            _movieRepository.Update(movieEntity);
            await _unitOfWork.CommitAsync();
        }

        public MovieResponseModel Get(long entityId)
        {
            var movieEntity = _movieRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(movieEntity);

            return _mapper.Map<MovieResponseModel>(movieEntity);
        }

        public IEnumerable<MovieResponseModel> GetAll()
        {
            var movieEntities = _movieRepository.GetAll();
            
            return movieEntities.Select(movieEntity => _mapper.Map<MovieResponseModel>(movieEntity)).ToList();
        }
    }
}