﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models.Seat;
using Service.Models.Session;

namespace Service.Services
{
    public interface ISessionsService
    {
        public Task<CreateSessionResponseModel> AddAsync(CreateSessionModel createSessionModel);
        public Task DeleteAsync(long entityId);
        public Task UpdateAsync(UpdateSessionModel updateSessionModel);
        public SessionResponseModel Get(long entityId);
        public IEnumerable<SessionResponseModel> GetAll(long? cinemaId, long? movieId);
        public IEnumerable<SeatResponseModel>  GetFreeSeats(long entityId);
    }
}