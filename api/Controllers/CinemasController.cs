using System;
using System.Linq;
using System.Threading.Tasks;
using api.ViewModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Models.Cinema;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly IMapper _cinemasMapper;
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService, IMapper cinemasMapper)
        {
            _cinemasService = cinemasService;
            _cinemasMapper = cinemasMapper;
        }

        // GET: api/Cinemas
        [HttpGet]
        public IActionResult GetCinemas()
        {
            try
            {
                var cinemas = _cinemasService.GetAll();
                return Ok(cinemas.Select(cinema => _cinemasMapper.Map<CinemaGetResponse>(cinema)));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse());
            }
        }

        // GET: api/Cinemas/5
        [HttpGet("{id:long}")]
        public IActionResult GetCinema(long id)
        {
            try
            {
                var cinema = _cinemasService.Get(id);
                if (cinema == null) return NotFound();
                return Ok(_cinemasMapper.Map<CinemaGetResponse>(cinema));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse());
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
                return BadRequest(new ErrorResponse());
            }
        }

        // PUT: api/Cinemas/5
        // [Authorize(Roles = "admin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutCinema(long id, [FromBody] CinemaUpdateRequest cinemaUpdateRequest)
        {
            try
            {
                if (id != cinemaUpdateRequest.Id) return BadRequest();
                var cinema = _cinemasMapper.Map<Cinema>(cinemaUpdateRequest);
                await _cinemasService.UpdateAsync(cinema);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse());
            }
        }

        // DELETE: api/Cinemas/5
        // [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            try
            {
                await _cinemasService.DeleteAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                if (e is NotFoundException) return NotFound();
                return BadRequest(new ErrorResponse());
            }
        }
    }
}