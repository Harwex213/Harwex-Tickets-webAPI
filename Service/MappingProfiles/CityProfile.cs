using AutoMapper;
using Domain.Entities;
using Service.Models.Cinema;
using Service.Models.City;

namespace Service.MappingProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CreateCityModel, City>();
            CreateMap<City, CreateCityResponseModel>();
            
            CreateMap<UpdateCityModel, City>();
            
            CreateMap<City, CityResponseModel>();
        }
    }
}