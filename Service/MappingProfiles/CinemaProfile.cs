using AutoMapper;
using Domain.Entities;
using Domain.Models.Cinema;

namespace Service.MappingProfiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaModel, Cinema>()
                .ForMember(c => c.Halls, opt => opt.Ignore());
            CreateMap<Cinema, CreateCinemaResponseModel>();
        }
    }
}