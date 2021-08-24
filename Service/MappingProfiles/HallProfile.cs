using AutoMapper;
using Domain.Entities;
using Service.Models.Hall;

namespace Service.MappingProfiles
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<CreateHallModel, Hall>();
            CreateMap<CreateHallModel, HallModel>();
            CreateMap<Hall, CreateHallResponseModel>();

            CreateMap<UpdateHallModel, Hall>();
            CreateMap<UpdateHallModel, HallModel>();
            
            CreateMap<Hall, HallModel>();
        }
    }
}