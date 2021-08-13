using api.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CinemaCreateRequest, Cinema>();
            CreateMap<CinemaUpdateRequest, Cinema>();
            CreateMap<Cinema, CinemaGetResponse>();
            CreateMap<Cinema, CinemaCreateResponse>();
            
        }
    }
}