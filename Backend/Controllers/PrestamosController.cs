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
    public class PrestamosController : ControllerBase
    {
        private readonly BiblioContext _context;

        public PrestamosController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            return await _context.Prestamos
                .Include(p => p.Ejemplar)
                .ThenInclude(e => e.Libro)
                .AsNoTracking().Where(p => !p.IsDeleted).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetDeletedsPrestamos()
        {
            return await _context.Prestamos.AsNoTracking().IgnoreQueryFilters().Where(p => p.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _context.Prestamos
             .Include(p => p.Ejemplar)
             .ThenInclude(e => e.Libro)
            .AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (prestamo == null)
            {
                return NotFound();
            }
            return prestamo;
        }

        [HttpGet("byusuario")]
        public async Task<ActionResult<List<Prestamo>>> GetByUsuario([FromQuery] int idusuario = 0)
        {
            if (idusuario == 0)
            {
                return BadRequest("El parametro del usuario es obligatorio");
            }
            var prestamos = await _context.Prestamos
                .Include(p => p.Ejemplar)
                .ThenInclude(e => e.Libro)
                .AsNoTracking()
                .Where(p => p.UsuarioId.Equals(idusuario))
                .ToListAsync();

            return prestamos;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            _context.TryAttach(prestamo?.Usuario);
            _context.TryAttach(prestamo?.Ejemplar);
            if (id != prestamo.Id)
            {
                return BadRequest();
            }
            _context.Entry(prestamo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestamoExists(id))
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
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
        {
            _context.TryAttach(prestamo?.Usuario);
            _context.TryAttach(prestamo?.Ejemplar);
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPrestamo", new { id = prestamo.Id }, prestamo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.IsDeleted = true;
            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestorePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.IsDeleted = false;
            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
