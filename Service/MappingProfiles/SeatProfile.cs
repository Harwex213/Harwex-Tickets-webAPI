using AutoMapper;
using Domain.Entities;
using Service.Models.Seat;

namespace Service.MappingProfiles
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Seat, SeatResponseModel>();
        }
    }
}