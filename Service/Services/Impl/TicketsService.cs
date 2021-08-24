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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Ticket> _ticketRepository;

        public TicketsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ticketRepository = unitOfWork.Repository<Ticket>();
        }

        public async Task<CreateTicketResponseModel> AddAsync(CreateTicketModel createTicketModel)
        {
            var ticketEntity = _mapper.Map<Ticket>(createTicketModel);
            
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
    }
}