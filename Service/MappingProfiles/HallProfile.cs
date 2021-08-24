using AutoMapper;
using Domain.Entities;
using Service.Models.Hall;

namespace Service.MappingProfiles
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<Hall, HallModel>();
            
            CreateMap<UpdateHallModel, Hall>();
            
            CreateMap<Hall, CreateHallResponseModel>();
        }
    }
}