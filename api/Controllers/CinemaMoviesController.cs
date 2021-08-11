using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaMoviesController : ControllerBase
    {
        private readonly HarwexTicketsApiContext _context;

        public CinemaMoviesController(HarwexTicketsApiContext context)
        {
            _context = context;
        }

        // GET: api/CinemaMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaMovie>>> GetCinemaMovies()
        {
            return await _context.CinemaMovies.ToListAsync();
        }

        // GET: api/CinemaMovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CinemaMovie>> GetCinemaMovie(long id)
        {
            var cinemaMovie = await _context.CinemaMovies.FindAsync(id);

            if (cinemaMovie == null)
            {
                return NotFound();
            }

            return cinemaMovie;
        }

        // PUT: api/CinemaMovies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinemaMovie(long id, CinemaMovie cinemaMovie)
        {
            if (id != cinemaMovie.Id)
            {
                return BadRequest();
            }

            _context.Entry(cinemaMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaMovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CinemaMovies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CinemaMovie>> PostCinemaMovie(CinemaMovie cinemaMovie)
        {
            _context.CinemaMovies.Add(cinemaMovie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCinemaMovie", new { id = cinemaMovie.Id }, cinemaMovie);
        }

        // DELETE: api/CinemaMovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinemaMovie(long id)
        {
            var cinemaMovie = await _context.CinemaMovies.FindAsync(id);
            if (cinemaMovie == null)
            {
                return NotFound();
            }

            _context.CinemaMovies.Remove(cinemaMovie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CinemaMovieExists(long id)
        {
            return _context.CinemaMovies.Any(e => e.Id == id);
        }
    }
}
