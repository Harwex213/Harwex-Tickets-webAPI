using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Service.Exceptions;
using Service.Models.Cinema;
using Service.Models.Hall;

namespace Service.Services.Impl
{
    public class CinemasService : ICinemasService
    {
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRepository<Hall> _hallRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CinemasService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cinemaRepository = unitOfWork.Repository<Cinema>();
            _hallRepository = unitOfWork.Repository<Hall>();
            _seatRepository = unitOfWork.Repository<Seat>();
            _sessionRepository = unitOfWork.Repository<Session>();
            _ticketRepository = unitOfWork.Repository<Ticket>();
            _mapper = mapper;
        }

        public CinemaResponseModel Get(long entityId)
        {
            var cinemaEntity = _cinemaRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(cinemaEntity);

            return _mapper.Map<CinemaResponseModel>(cinemaEntity);
        }

        public IEnumerable<CinemaResponseModel> GetAll()
        {
            var cinemas = _cinemaRepository.GetAll();

            return cinemas.Select(_mapper.Map<CinemaResponseModel>).ToList();
        }

        public async Task<CreateCinemaResponseModel> AddAsync(CreateCinemaModel createCinemaModel)
        {
            var cinemaEntity = _mapper.Map<Cinema>(createCinemaModel);

            AddHalls(createCinemaModel, cinemaEntity);

            _cinemaRepository.Add(cinemaEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CreateCinemaResponseModel>(cinemaEntity);
        }

        public async Task DeleteAsync(long entityId)
        {
            var cinemaEntity = _cinemaRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(cinemaEntity);

            DeleteCinema(cinemaEntity);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteHallAsync(long hallId)
        {
            var hallEntity = _hallRepository.Find(hallId);
            ExceptionChecker.CheckEntityOnNull(hallEntity);
            
            DeleteHall(hallEntity);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateCinemaModel updateCinemaModel)
        {
            var cinemaEntity = _mapper.Map<Cinema>(updateCinemaModel);
            
            _cinemaRepository.Update(cinemaEntity);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateHallAsync(UpdateHallModel updateHallModel)
        {
            var hallEntity = _mapper.Map<Hall>(updateHallModel);
            
            _hallRepository.Update(hallEntity);
            
            await _unitOfWork.CommitAsync();
        }

        private void AddHalls(CreateCinemaModel cinemaModel, Cinema cinemaEntity, bool addToRepository = false)
        {
            foreach (var hallModel in cinemaModel.Halls)
            {
                var hallEntity = new Hall
                {
                    ColsAmount = hallModel.ColsAmount,
                    RowsAmount = hallModel.RowsAmount
                };

                for (var i = 0; i < hallModel.RowsAmount; i++)
                {
                    for (var j = 0; j < hallModel.ColsAmount; j++)
                    {
                        var seatEntity = new Seat
                        {
                            Row = i,
                            Position = j
                        };

                        hallEntity.Seats.Add(seatEntity);
                        if (addToRepository)
                        {
                            _seatRepository.Add(seatEntity);
                        }
                    }
                }

                cinemaEntity.Halls.Add(hallEntity);
                if (addToRepository)
                {
                    _hallRepository.Add(hallEntity);
                }
            }
        }

        private void DeleteCinema(Cinema cinemaEntity)
        {
            foreach (var hallEntity in cinemaEntity.Halls)
            {
                DeleteHall(hallEntity);
            }
            _cinemaRepository.Delete(cinemaEntity);
        }

        private void DeleteHall(Hall hallEntity)
        {
            DeleteSeats(hallEntity);
            DeleteSessions(hallEntity);
            _hallRepository.Delete(hallEntity);
        }

        private void DeleteSeats(Hall hallEntity)
        {
            foreach (var seatEntity in hallEntity.Seats)
            {
                _seatRepository.Delete(seatEntity);
            }
        }

        private void DeleteSessions(Hall hallEntity)
        {
            foreach (var sessionEntity in hallEntity.Sessions)
            {
                DeleteTickets(sessionEntity);
                _sessionRepository.Delete(sessionEntity);
            }
        }

        private void DeleteTickets(Session sessionEntity)
        {
            foreach (var ticketEntity in sessionEntity.Tickets)
            {
                _ticketRepository.Delete(ticketEntity);
            }
        }
    }
}