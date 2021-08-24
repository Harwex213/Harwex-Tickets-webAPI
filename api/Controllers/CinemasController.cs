using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers.Abstract;
using api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Models.Cinema;
using Service.Models.Hall;
using Service.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ExceptionHandlerController
    {
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

        // GET: api/Cinemas
        [HttpGet]
        public ActionResult<IEnumerable<CinemaResponseModel>> GetCinemas()
        {
            try
            {
                return Ok(_cinemasService.GetAll());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // GET: api/Cinemas/5
        [HttpGet("{id:long}")]
        public ActionResult<CinemaResponseModel> GetCinema(long id)
        {
            try
            {
                var cinemaResponseModel = _cinemasService.Get(id);
                if (cinemaResponseModel == null)
                {
                    return NotFound(new ErrorResponse("Not Found"));
                }

                return Ok(cinemaResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // POST: api/Cinemas
        // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreateCinemaResponseModel>> PostCinema(
            [FromBody] CreateCinemaModel createCinemaModel)
        {
            try
            {
                var cinemaResponseModel = await _cinemasService.AddAsync(createCinemaModel);
                return CreatedAtAction(nameof(GetCinema), new {id = cinemaResponseModel.Id},
                    cinemaResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
        
        // POST: api/Cinemas/halls
        // [Authorize(Roles = "admin")]
        [HttpPost("halls")]
        public async Task<ActionResult<CreateHallResponseModel>> PostCinema(
            [FromBody] CreateHallModel createHallModel)
        {
            try
            {
                var hallResponseModel = await _cinemasService.AddHallAsync(createHallModel);
                return CreatedAtAction(nameof(GetCinema), new {id = hallResponseModel.Id},
                    hallResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // PUT: api/Cinemas/5
        // [Authorize(Roles = "admin")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> PutCinema(long id,
            [FromBody] UpdateCinemaModel updateCinemaModel)
        {
            try
            {
                if (id != updateCinemaModel.Id)
                {
                    return BadRequest(new ErrorResponse("Id must match"));
                }

                await _cinemasService.UpdateAsync(updateCinemaModel);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
        
        // PUT: api/Cinemas/halls/5
        // [Authorize(Roles = "admin")]
        [HttpPut("halls/{id:long}")]
        public async Task<ActionResult<SuccessResponse>> PutHall(long id,
            [FromBody] UpdateHallModel updateHallModel)
        {
            try
            {
                if (id != updateHallModel.Id)
                {
                    return BadRequest(new ErrorResponse("Id must match"));
                }

                await _cinemasService.UpdateHallAsync(updateHallModel);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // DELETE: api/Cinemas/5
        // [Authorize(Roles = "admin")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> DeleteCinema(long id)
        {
            try
            {
                await _cinemasService.DeleteAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
        
        // DELETE: api/Cinemas/halls/5
        // [Authorize(Roles = "admin")]
        [HttpDelete("halls/{id:long}")]
        public async Task<ActionResult<SuccessResponse>> DeleteHall(long id)
        {
            try
            {
                await _cinemasService.DeleteHallAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
    }
}