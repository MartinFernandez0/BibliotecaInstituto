using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.ExtensionMethod;
using Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EjemplaresController : ControllerBase
    {
        private readonly BiblioContext _context;

        public EjemplaresController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ejemplar>>> GetEjemplares([FromQuery] bool? disponible = null)
        {
            var query = _context.Ejemplares.AsNoTracking().Where(e => !e.IsDeleted);
            if (disponible.HasValue)
                query = query.Where(e => e.Disponible == disponible.Value);
            return await query.ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Ejemplar>>> GetDeletedsEjemplares()
        {
            return await _context.Ejemplares.AsNoTracking().IgnoreQueryFilters().Where(e => e.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ejemplar>> GetEjemplar(int id)
        {
            var ejemplar = await _context.Ejemplares.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (ejemplar == null)
            {
                return NotFound();
            }
            return ejemplar;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjemplar(int id, Ejemplar ejemplar)
        {
            _context.TryAttach(ejemplar?.Libro);
            if (id != ejemplar.Id)
            {
                return BadRequest();
            }
            _context.Entry(ejemplar).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjemplarExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Ejemplar>> PostEjemplar(Ejemplar ejemplar)
        {
            _context.TryAttach(ejemplar?.Libro);
            _context.Ejemplares.Add(ejemplar);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEjemplar", new { id = ejemplar.Id }, ejemplar);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjemplar(int id)
        {
            var ejemplar = await _context.Ejemplares.FindAsync(id);
            if (ejemplar == null)
            {
                return NotFound();
            }
            ejemplar.IsDeleted = true;
            _context.Ejemplares.Update(ejemplar);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreEjemplar(int id)
        {
            var ejemplar = await _context.Ejemplares.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (ejemplar == null)
            {
                return NotFound();
            }
            ejemplar.IsDeleted = false;
            _context.Ejemplares.Update(ejemplar);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EjemplarExists(int id)
        {
            return _context.Ejemplares.Any(e => e.Id == id);
        }
    }
}
