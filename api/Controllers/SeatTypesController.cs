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
    public class SeatTypesController : ControllerBase
    {
        private readonly HarwexTicketsApiContext _context;

        public SeatTypesController(HarwexTicketsApiContext context)
        {
            _context = context;
        }

        // GET: api/SeatTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatType>>> GetSeatTypes()
        {
            return await _context.SeatTypes.ToListAsync();
        }

        // GET: api/SeatTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeatType>> GetSeatType(long id)
        {
            var seatType = await _context.SeatTypes.FindAsync(id);

            if (seatType == null) return NotFound();

            return seatType;
        }

        // PUT: api/SeatTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeatType(long id, SeatType seatType)
        {
            if (id != seatType.Id) return BadRequest();

            _context.Entry(seatType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatTypeExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/SeatTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SeatType>> PostSeatType(SeatType seatType)
        {
            _context.SeatTypes.Add(seatType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeatType), new {id = seatType.Id}, seatType);
        }

        // DELETE: api/SeatTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeatType(long id)
        {
            var seatType = await _context.SeatTypes.FindAsync(id);
            if (seatType == null) return NotFound();

            _context.SeatTypes.Remove(seatType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatTypeExists(long id)
        {
            return _context.SeatTypes.Any(e => e.Id == id);
        }
    }
}