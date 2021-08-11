using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsControllers : ControllerBase
    {
        private readonly HarwexTicketsApiContext _context;

        public HallsControllers(HarwexTicketsApiContext context)
        {
            _context = context;
        }

        // GET: api/HallsControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHalls()
        {
            return await _context.Halls.ToListAsync();
        }

        // GET: api/HallsControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hall>> GetHall(long id)
        {
            var hall = await _context.Halls.FindAsync(id);

            if (hall == null) return NotFound();

            return hall;
        }

        // PUT: api/HallsControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHall(long id, Hall hall)
        {
            if (id != hall.Id) return BadRequest();

            _context.Entry(hall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/HallsControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hall>> PostHall(Hall hall)
        {
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHall), new {id = hall.Id}, hall);
        }

        // DELETE: api/HallsControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(long id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) return NotFound();

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallExists(long id)
        {
            return _context.Halls.Any(e => e.Id == id);
        }
    }
}