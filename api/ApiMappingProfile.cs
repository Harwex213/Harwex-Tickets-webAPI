using api.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CityMapping();
            AuthMapping();
        }

        private void CityMapping()
        {
            CreateMap<City, CityGetResponse>();
        }

        private void AuthMapping()
        {
            CreateMap<AuthRegisterRequest, User>()
                .ForMember("PasswordHash", opt => opt.MapFrom(r => r.Password));
        }
    }
}