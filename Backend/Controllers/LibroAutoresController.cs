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
    public class LibroAutoresController : ControllerBase
    {
        private readonly BiblioContext _context;

        public LibroAutoresController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroAutor>>> GetLibroAutores([FromQuery] string filtro = "")
        {
            return await _context.LibroAutores.Include(l => l.Autor).Include(l => l.Libro).AsNoTracking().Where(l => l.Libro.Titulo.Contains(filtro.ToUpper()) || l.Autor.Nombre.Contains(filtro.ToUpper())).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<LibroAutor>>> GetDeletedsLibroAutores()
        {
            return await _context.LibroAutores.AsNoTracking().IgnoreQueryFilters().Where(la => la.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroAutor>> GetLibroAutor(int id)
        {
            var libroAutor = await _context.LibroAutores.AsNoTracking().FirstOrDefaultAsync(la => la.Id.Equals(id));
            if (libroAutor == null)
            {
                return NotFound();
            }
            return libroAutor;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibroAutor(int id, LibroAutor libroAutor)
        {
            _context.TryAttach(libroAutor?.Libro);
            _context.TryAttach(libroAutor?.Autor);
            _context.TryAttach(libroAutor?.Libro?.Editorial);
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

        [HttpPost]
        public async Task<ActionResult<LibroAutor>> PostLibroAutor(LibroAutor libroAutor)
        {
            _context.TryAttach(libroAutor?.Libro);
            _context.TryAttach(libroAutor?.Autor);
            _context.TryAttach(libroAutor?.Libro?.Editorial);
            _context.LibroAutores.Add(libroAutor);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLibroAutor", new { id = libroAutor.Id }, libroAutor);
        }

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

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreLibroAutor(int id)
        {
            var libroAutor = await _context.LibroAutores.IgnoreQueryFilters().FirstOrDefaultAsync(la => la.Id.Equals(id));
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
            return _context.LibroAutores.Any(e => e.Id == id);
        }
    }
}
