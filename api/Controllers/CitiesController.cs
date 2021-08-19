using System;
using System.Collections.Generic;
using System.Linq;
using api.ViewModel;
using AutoMapper;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _cinemasMapper;
        private readonly ICitiesService _citiesService;

        public CitiesController(IMapper cinemasMapper, ICitiesService citiesService)
        {
            _cinemasMapper = cinemasMapper;
            _citiesService = citiesService;
        }

        // GET: api/Cities
        [HttpGet]
        public IActionResult GetCities(string cityName)
        {
            try
            {
                if (cityName != null)
                {
                    var city = _citiesService.GetByNameCity(cityName);
                    var cityGetResponse = _cinemasMapper.Map<CityGetResponse>(city);
                    var response = new List<CityGetResponse>();
                    if (cityGetResponse != null) response.Add(cityGetResponse);
                    return Ok(response);
                }

                var cities = _citiesService.GetAll();
                return Ok(cities.Select(city => _cinemasMapper.Map<CityGetResponse>(city)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}