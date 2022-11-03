using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Service.Exceptions;
using Service.Models.Ticket;

namespace Service.Services.Impl
{
    public class TicketsService : ITicketsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TicketsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ticketRepository = unitOfWork.Repository<Ticket>();
        }

        public async Task<CreateTicketResponseModel> AddAsync(CreateTicketModel createTicketModel)
        {
            var ticketEntity = _mapper.Map<Ticket>(createTicketModel);
            if (CheckTicketOnOrdered(ticketEntity))
            {
                throw new ConflictException("Ticket was ordered");
            }

            _ticketRepository.Add(ticketEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CreateTicketResponseModel>(ticketEntity);
        }

        public async Task DeleteAsync(long entityId)
        {
            var ticketEntity = _ticketRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(ticketEntity);

            _ticketRepository.Delete(ticketEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateTicketModel updateTicketModel)
        {
            var ticketEntity = _mapper.Map<Ticket>(updateTicketModel);
            if (CheckTicketOnOrdered(ticketEntity))
            {
                throw new ConflictException("Ticket was ordered");
            }

            _ticketRepository.Update(ticketEntity);
            await _unitOfWork.CommitAsync();
        }

        public TicketResponseModel Get(long entityId)
        {
            var ticketEntity = _ticketRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(ticketEntity);

            return _mapper.Map<TicketResponseModel>(ticketEntity);
        }

        public IEnumerable<TicketResponseModel> GetAll()
        {
            var ticketEntities = _ticketRepository.GetAll();

            return ticketEntities.Select(ticketEntity => _mapper.Map<TicketResponseModel>(ticketEntity)).ToList();
        }

        private bool CheckTicketOnOrdered(Ticket ticketEntity)
        {
            var orderedTicket = _ticketRepository.List(ticket =>
                ticket.SessionId == ticketEntity.SessionId &&
                ticket.SeatId == ticketEntity.SeatId).FirstOrDefault();
            return orderedTicket != null;
        }
    }
}