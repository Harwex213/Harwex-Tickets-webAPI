using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Service.Exceptions;
using Service.Models.City;

namespace Service.Services.Impl
{
    public class CitiesService : ICitiesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<City> _cityRepository;

        public CitiesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cityRepository = unitOfWork.Repository<City>();
        }

        public async Task<CreateCityResponseModel> AddAsync(CreateCityModel createCityModel)
        {
            var cityEntity = _mapper.Map<City>(createCityModel);
            
            _cityRepository.Add(cityEntity);
            await _unitOfWork.CommitAsync();
            
            return _mapper.Map<CreateCityResponseModel>(cityEntity);
        }

        public async Task DeleteAsync(long entityId)
        {
            var cityEntity = _cityRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(cityEntity);
            
            _cityRepository.Delete(cityEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(UpdateCityModel updateCityModel)
        {
            var cityEntity = _mapper.Map<City>(updateCityModel);
            
            _cityRepository.Update(cityEntity);
            await _unitOfWork.CommitAsync();
        }

        public CityResponseModel Get(long entityId)
        {
            var cityEntity = _cityRepository.Find(entityId);
            ExceptionChecker.CheckEntityOnNull(cityEntity);

            return _mapper.Map<CityResponseModel>(cityEntity);
        }

        public IEnumerable<CityResponseModel> GetAll()
        {
            var cityEntities = _cityRepository.GetAll();
            
            return cityEntities.Select(cityEntity => _mapper.Map<CityResponseModel>(cityEntity)).ToList();
        }
        
        
        public IEnumerable<CityResponseModel> GetAllByName(string name)
        {
            var cityEntities = _cityRepository.List(city => city.Name == name);
            
            return cityEntities.Select(cityEntity => _mapper.Map<CityResponseModel>(cityEntity)).ToList();
        }
    }
}