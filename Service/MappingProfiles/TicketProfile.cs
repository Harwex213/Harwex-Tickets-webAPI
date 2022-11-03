using AutoMapper;
using Domain.Entities;
using Service.Models.Ticket;

namespace Service.MappingProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            
            CreateMap<CreateTicketModel, Ticket>();
            CreateMap<Ticket, CreateTicketResponseModel>();
            
            CreateMap<UpdateTicketModel, Ticket>();
            
            CreateMap<Ticket, TicketResponseModel>();
        }
    }
}