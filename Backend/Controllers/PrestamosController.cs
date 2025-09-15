using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DataContext;
using Service.Models;
using Service.ExtensionMethod;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public PrestamosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/Prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos([FromQuery] string filtro = "")
        {
            return await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Ejemplar)
                .ThenInclude(e => e.Libro)
                .AsNoTracking()
                .Where(p => p.Usuario.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                            p.Ejemplar.Libro.Titulo.ToUpper().Contains(filtro.ToUpper()))
                .ToListAsync();
        }

        // GET: api/Prestamos/deleted
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetDeletedPrestamos()
        {
            return await _context.Prestamos
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted).ToListAsync();
        }

        // GET: api/Prestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _context.Prestamos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return prestamo;
        }

        // PUT: api/Prestamos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return BadRequest();
            }
            _context.TryAttach(prestamo.Usuario);
            _context.TryAttach(prestamo.Ejemplar);
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

        // POST: api/Prestamos
        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
        {
            _context.TryAttach(prestamo.Usuario);
            _context.TryAttach(prestamo.Ejemplar);
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPrestamo", new { id = prestamo.Id }, prestamo);
        }

        // DELETE: api/Prestamos/5 (soft delete)
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

        // PUT: api/Prestamos/restore/5
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestorePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);
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
            return _context.Prestamos.Any(p => p.Id == id);
        }
    }
}
