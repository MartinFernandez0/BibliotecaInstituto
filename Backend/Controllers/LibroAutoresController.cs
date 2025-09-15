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
    public class LibroAutoresController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LibroAutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/LibroAutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroAutor>>> GetLibroAutores([FromQuery] string filtro="")
        {
            return await _context.LibroAutores
                .Include(l =>l.Autor)
                .Include(l => l.Libro)
                .AsNoTracking()
                .Where(l => l.Libro.Titulo.ToUpper().Contains(filtro.ToUpper()) ||
                       l.Autor.Nombre.ToUpper().Contains(filtro.ToUpper())).ToListAsync();
        }

        // GET: api/LibroAutores/deleted
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<LibroAutor>>> GetDeletedLibroAutores()
        {
            return await _context.LibroAutores
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(l => l.IsDeleted).ToListAsync();
        }

        // GET: api/LibroAutores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroAutor>> GetLibroAutor(int id)
        {
            var libroAutor = await _context.LibroAutores.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

            if (libroAutor == null)
            {
                return NotFound();
            }

            return libroAutor;
        }

        // PUT: api/LibroAutores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibroAutor(int id, LibroAutor libroAutor)
        {
            if (id != libroAutor.Id)
            {
                return BadRequest();
            }

            _context.Entry(libroAutor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroAutorExists(id))
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

        // POST: api/LibroAutores
        [HttpPost]
        public async Task<ActionResult<LibroAutor>> PostLibroAutor(LibroAutor libroAutor)
        {
            // Attach sirve para ""
            _context.TryAttach(libroAutor?.Libro);
            _context.TryAttach(libroAutor?.Libro.Editorial);
            _context.TryAttach(libroAutor?.Autor);
            _context.LibroAutores.Add(libroAutor);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibroAutor", new { id = libroAutor.Id }, libroAutor);
        }

        // DELETE: api/LibroAutores/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibroAutor(int id)
        {
            var libroAutor = await _context.LibroAutores.FindAsync(id);
            if (libroAutor == null)
            {
                return NotFound();
            }

            libroAutor.IsDeleted = true;
            _context.LibroAutores.Update(libroAutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/LibroAutores/restore/5
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreLibroAutor(int id)
        {
            var libroAutor = await _context.LibroAutores.IgnoreQueryFilters().FirstOrDefaultAsync(l => l.Id == id);
            if (libroAutor == null)
            {
                return NotFound();
            }
            libroAutor.IsDeleted = false;
            _context.LibroAutores.Update(libroAutor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool LibroAutorExists(int id)
        {
            return _context.LibroAutores.Any(l => l.Id == id);
        }
    }
}
