using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers.Abstract;
using api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Models.City;
using Service.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ExceptionHandlerController
    {
        private readonly ICitiesService _citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        // GET: api/Cities
        [HttpGet]
        public ActionResult<IEnumerable<CityResponseModel>> GetCities()
        {
            try
            {
                return Ok(_citiesService.GetAll());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // GET: api/Cities/5
        [HttpGet("{id:long}")]
        public ActionResult<CityResponseModel> GetCity(long id)
        {
            try
            {
                var cityResponseModel = _citiesService.Get(id);
                if (cityResponseModel == null)
                {
                    return NotFound(new ErrorResponse("Not Found"));
                }

                return Ok(cityResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // POST: api/Cities
        // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreateCityResponseModel>> PostCity(
            [FromBody] CreateCityModel createCityModel)
        {
            try
            {
                var cityResponseModel = await _citiesService.AddAsync(createCityModel);
                return CreatedAtAction(nameof(GetCity), new {id = cityResponseModel.Id},
                    cityResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // PUT: api/Cities/5
        // [Authorize(Roles = "admin")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> PutCity(long id,
            [FromBody] UpdateCityModel updateCityModel)
        {
            try
            {
                if (id != updateCityModel.Id)
                {
                    return BadRequest(new ErrorResponse("Id must match"));
                }

                await _citiesService.UpdateAsync(updateCityModel);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // DELETE: api/Cities/5
        // [Authorize(Roles = "admin")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> DeleteCity(long id)
        {
            try
            {
                await _citiesService.DeleteAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
    }
}