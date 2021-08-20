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
    public class CinemaService : ICinemasService
    {
        private readonly IRepository<Cinema> _cinemaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CinemaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cinemaRepository = unitOfWork.Repository<Cinema>();
            _mapper = mapper;
        }

        public CinemaResponseModel Get(long entityId)
        {
            var cinemaEntity = _cinemaRepository.Find(entityId);
            if (cinemaEntity == null)
            {
                throw new NotFoundException("Not found");
            }
            cinemaEntity.Halls = _unitOfWork.Repository<Hall>().List(hall => hall.CinemaId == cinemaEntity.Id)
                .ToHashSet();
            return (CinemaResponseModel) GenerateFromCinema(cinemaEntity);
        }

        public IEnumerable<CinemaResponseModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CreateCinemaResponseModel> AddAsync(CreateCinemaModel createCinemaModel)
        {
            var cinemaEntity = GenerateToCinema(createCinemaModel);
            _cinemaRepository.Add(cinemaEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CreateCinemaResponseModel>(cinemaEntity);
        }

        public async Task DeleteAsync(long entityId)
        {
            var cinemaEntity = _cinemaRepository.Find(entityId);
            if (cinemaEntity == null)
            {
                throw new NotFoundException();
            }

            _cinemaRepository.Delete(cinemaEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateCinemaModel updateCinemaModel)
        {
            var cinemaEntity = GenerateToCinema(updateCinemaModel);
            _cinemaRepository.Update(cinemaEntity);
            await _unitOfWork.CommitAsync();
        }

        private Cinema GenerateToCinema(ICinemaGeneratableModel generatableModel)
        {
            var cinemaEntity = _mapper.Map<Cinema>(generatableModel);

            foreach (var hallModel in generatableModel.Halls)
            {
                var hall = new Hall
                {
                    ColsAmount = hallModel.ColsAmount,
                    RowsAmount = hallModel.RowsAmount
                };
                for (var i = 0; i < hallModel.RowsAmount; i++)
                {
                    for (var j = 0; j < hallModel.ColsAmount; j++)
                    {
                        hall.Seats.Add(new Seat {Row = i, Position = j});
                    }
                }

                cinemaEntity.Halls.Add(hall);
            }

            return cinemaEntity;
        }

        private ICinemaGeneratableModel GenerateFromCinema(Cinema cinemaEntity, Type typeToGenerate)
        {
            var generatableModel = _mapper.Map();

            foreach (var hall in cinemaEntity.Halls)
            {
                var hallModel = new HallModel
                {
                    RowsAmount = hall.RowsAmount,
                    ColsAmount = hall.ColsAmount
                };
                
                generatableModel.Halls.Add(hallModel);
            }

            return generatableModel;
        }
    }
}