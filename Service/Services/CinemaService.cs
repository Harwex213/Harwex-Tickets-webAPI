using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models.Cinema;
using Service.Services.Abstract;

namespace Service.Services
{
    public class CinemaService : CrudService<Cinema>, ICinemasService
    {
        private readonly IMapper _mapper;

        public CinemaService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<CreateCinemaResponseModel> AddAsync(CreateCinemaModel createCinemaModel)
        {
            var cinemaEntity = _mapper.Map<Cinema>(createCinemaModel);

            foreach (var hallModel in createCinemaModel.Halls)
            {
                var hall = new Hall();
                for (var i = 0; i < hallModel.RowsAmount; i++)
                {
                    for (var j = 0; j < hallModel.ColsAmount; j++)
                    {
                        hall.Seats.Add(new Seat { Row = i, Position = j});
                    }
                }

                cinemaEntity.Halls.Add(hall);
            }

            await AddAsync(cinemaEntity);
            var cinemaResponseModel = _mapper.Map<CreateCinemaResponseModel>(cinemaEntity);
            return cinemaResponseModel;
        }
    }
}