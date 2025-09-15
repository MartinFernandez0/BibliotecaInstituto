using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenerosController : ControllerBase
    {
        private readonly BiblioContext _context;

        public GenerosController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros([FromQuery] string filtro = "")
        {
            return await _context.Generos.AsNoTracking().Where(g => g.Nombre.Contains(filtro)).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetDeletedsGeneros([FromQuery] string filtro = "")
        {
            return await _context.Generos.AsNoTracking().IgnoreQueryFilters().Where(g => g.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _context.Generos.AsNoTracking().FirstOrDefaultAsync(g => g.Id.Equals(id));
            if (genero == null)
            {
                return NotFound();
            }
            return genero;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            if (id != genero.Id)
            {
                return BadRequest();
            }
            _context.Entry(genero).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
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
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGenero", new { id = genero.Id }, genero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            genero.IsDeleted = true;
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreGenero(int id)
        {
            var genero = await _context.Generos.IgnoreQueryFilters().FirstOrDefaultAsync(g => g.Id.Equals(id));
            if (genero == null)
            {
                return NotFound();
            }
            genero.IsDeleted = false;
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
}
