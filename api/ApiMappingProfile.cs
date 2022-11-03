using api.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            AuthMapping();
        }

        private void AuthMapping()
        {
            CreateMap<AuthRegisterRequest, User>()
                .ForMember("PasswordHash", opt => opt.MapFrom(r => r.Password));
        }
    }
}