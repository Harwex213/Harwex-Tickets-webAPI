using api.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CinemaMapping();
            AuthMapping();
        }

        private void CinemaMapping()
        {
            CreateMap<CinemaCreateRequest, Cinema>();
            CreateMap<CinemaUpdateRequest, Cinema>();
            CreateMap<Cinema, CinemaGetResponse>();
            CreateMap<Cinema, CinemaCreateResponse>();
        }

        private void AuthMapping()
        {
            CreateMap<AuthRegisterRequest, User>()
                .ForMember("PasswordHash", opt => opt.MapFrom(r => r.Password));
        }
    }
}