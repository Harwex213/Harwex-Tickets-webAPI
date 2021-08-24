using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models.Ticket;

namespace Service.Services
{
    public interface ITicketsService
    {
        public Task<CreateTicketResponseModel> AddAsync(CreateTicketModel createTicketModel);
        public Task DeleteAsync(long entityId);
        public Task UpdateAsync(UpdateTicketModel updateTicketModel);
        public TicketResponseModel Get(long entityId);
        public IEnumerable<TicketResponseModel> GetAll();
    }
}