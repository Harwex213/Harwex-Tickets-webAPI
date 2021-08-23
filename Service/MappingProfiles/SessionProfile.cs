using AutoMapper;
using Domain.Entities;
using Service.Models.Session;

namespace Service.MappingProfiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<CreateSessionModel, Session>();
            CreateMap<Session, CreateSessionResponseModel>();
            
            CreateMap<UpdateSessionModel, Session>();
            
            CreateMap<Session, SessionResponseModel>();
        }
    }
}