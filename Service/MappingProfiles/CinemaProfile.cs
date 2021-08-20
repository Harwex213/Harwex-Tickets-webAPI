using AutoMapper;
using Domain.Entities;
using Service.Models.Cinema;

namespace Service.MappingProfiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaModel, Cinema>()
                .ForMember(c => c.Halls, opt => opt.Ignore());
            CreateMap<Cinema, CreateCinemaResponseModel>();
            
            CreateMap<UpdateCinemaModel, Cinema>();
            
            CreateMap<Cinema, CinemaResponseModel>();
        }
    }
}