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
    public class CinemaService : ICinemasService
    {
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IRepository<Hall> _hallRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Seat> _seatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CinemaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cinemaRepository = unitOfWork.Repository<Cinema>();
            _hallRepository = unitOfWork.Repository<Hall>();
            _seatRepository = unitOfWork.Repository<Seat>();
            _mapper = mapper;
        }

        public CinemaResponseModel Get(long entityId)
        {
            var cinemaEntity = _cinemaRepository.Find(entityId);
            CheckCinemaEntityOnNull(cinemaEntity);
            
            return GenerateCinemaResponseModel(cinemaEntity);
        }

        public IEnumerable<CinemaResponseModel> GetAll()
        {
            var cinemas = _cinemaRepository.GetAll();
            
            return cinemas.Select(GenerateCinemaResponseModel).ToList();
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
            CheckCinemaEntityOnNull(cinemaEntity);
            
            DeleteCinema(cinemaEntity);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateCinemaModel updateCinemaModel)
        {
            var cinemaEntity = _cinemaRepository.Find(updateCinemaModel.Id);
            CheckCinemaEntityOnNull(cinemaEntity);

            cinemaEntity.CityId = updateCinemaModel.CityId;
            cinemaEntity.Name = updateCinemaModel.Name;
            cinemaEntity.Halls = new List<Hall>();

            DeleteHalls(cinemaEntity);
            AddHalls(updateCinemaModel, cinemaEntity, true);

            _cinemaRepository.Update(cinemaEntity);
            await _unitOfWork.CommitAsync();
        }

        private void CheckCinemaEntityOnNull(Cinema cinemaEntity)
        {
            if (cinemaEntity == null)
            {
                throw new NotFoundException();
            }
        }

        private CinemaResponseModel GenerateCinemaResponseModel(Cinema cinemaEntity)
        {
            var cinemaModel = _mapper.Map<CinemaResponseModel>(cinemaEntity);
            cinemaModel.Halls = new List<HallModel>();

            foreach (var hall in cinemaEntity.Halls)
            {
                var hallModel = new HallModel
                {
                    RowsAmount = hall.RowsAmount,
                    ColsAmount = hall.ColsAmount
                };

                cinemaModel.Halls.Add(hallModel);
            }

            return cinemaModel;
        }

        private void AddHalls(IGeneratableCinemaModel cinemaModel, Cinema cinemaEntity, bool addToRepository = false)
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
            DeleteHalls(cinemaEntity);
            _cinemaRepository.Delete(cinemaEntity);
        }

        private void DeleteHalls(Cinema cinemaEntity)
        {
            foreach (var hallEntity in cinemaEntity.Halls)
            {
                DeleteSeats(hallEntity);
                _hallRepository.Delete(hallEntity);
            }
        }

        private void DeleteSeats(Hall hallEntity)
        {
            foreach (var seatEntity in hallEntity.Seats)
            {
                _seatRepository.Delete(seatEntity);
            }
        }
    }
}