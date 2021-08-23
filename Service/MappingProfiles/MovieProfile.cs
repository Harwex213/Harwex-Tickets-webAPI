using AutoMapper;
using Domain.Entities;
using Service.Models.Movie;

namespace Service.MappingProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieModel, Movie>();
            CreateMap<Movie, CreateMovieResponseModel>();
            
            CreateMap<UpdateMovieModel, Movie>();
            
            CreateMap<Movie, MovieResponseModel>();
        }
    }
}