using AutoMapper;
using Domain.Entities;
using Service.Models.Hall;

namespace Service.MappingProfiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Hall, HallModel>();
        }
    }
}