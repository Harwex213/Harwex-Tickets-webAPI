using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers.Abstract;
using api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Models.Movie;
using Service.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ExceptionHandlerController
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<MovieResponseModel>> GetMovies()
        {
            try
            {
                return Ok(_moviesService.GetAll());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id:long}")]
        public ActionResult<MovieResponseModel> GetMovie(long id)
        {
            try
            {
                var movieResponseModel = _moviesService.Get(id);
                if (movieResponseModel == null)
                {
                    return NotFound(new ErrorResponse("Not Found"));
                }

                return Ok(movieResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // POST: api/Movies
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreateMovieResponseModel>> PostMovie(
            [FromBody] CreateMovieModel createMovieModel)
        {
            try
            {
                var movieResponseModel = await _moviesService.AddAsync(createMovieModel);
                return CreatedAtAction(nameof(GetMovie), new {id = movieResponseModel.Id},
                    movieResponseModel);
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // PUT: api/Movies/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> PutMovie(long id,
            [FromBody] UpdateMovieModel updateMovieModel)
        {
            try
            {
                if (id != updateMovieModel.Id)
                {
                    return BadRequest(new ErrorResponse("Id must match"));
                }

                await _moviesService.UpdateAsync(updateMovieModel);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }

        // DELETE: api/Movies/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<SuccessResponse>> DeleteMovie(long id)
        {
            try
            {
                await _moviesService.DeleteAsync(id);
                return Ok(new SuccessResponse());
            }
            catch (Exception e)
            {
                return AnalyzeException(e);
            }
        }
    }
}