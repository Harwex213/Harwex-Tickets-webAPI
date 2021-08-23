using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Service.Exceptions;
using Service.Models.Session;

namespace Service.Services.Impl
{
    public class SessionsService : ISessionsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Session> _sessionRepository;

        public SessionsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sessionRepository = unitOfWork.Repository<Session>();
        }

        public async Task<CreateSessionResponseModel> AddAsync(CreateSessionModel createSessionModel)
        {
            var sessionEntity = _mapper.Map<Session>(createSessionModel);
            
            _sessionRepository.Add(sessionEntity);
            await _unitOfWork.CommitAsync();
            
            return _mapper.Map<CreateSessionResponseModel>(sessionEntity);
        }

        public async Task DeleteAsync(long entityId)
        {
            var sessionEntity = _sessionRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(sessionEntity);
            
            _sessionRepository.Delete(sessionEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateSessionModel updateSessionModel)
        {
            var sessionEntity = _mapper.Map<Session>(updateSessionModel);
            
            _sessionRepository.Update(sessionEntity);
            await _unitOfWork.CommitAsync();
        }

        public SessionResponseModel Get(long entityId)
        {
            var sessionEntity = _sessionRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(sessionEntity);

            return _mapper.Map<SessionResponseModel>(sessionEntity);
        }

        public IEnumerable<SessionResponseModel> GetAll()
        {
            var sessionEntities = _sessionRepository.GetAll();
            
            return sessionEntities.Select(SessionEntity => _mapper.Map<SessionResponseModel>(SessionEntity)).ToList();
        }
    }
}