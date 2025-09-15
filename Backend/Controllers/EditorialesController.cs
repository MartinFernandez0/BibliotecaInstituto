using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DataContext;
using Service.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialesController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public EditorialesController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/Editoriales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditoriales([FromQuery] string filtro = "")
        {
            return await _context.Editoriales.AsNoTracking().Where(e => e.Nombre.Contains(filtro)).ToListAsync();
        }

        // GET: api/Editoriales/deleted
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetDeletedEditoriales()
        {
            return await _context.Editoriales
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(e => e.IsDeleted).ToListAsync();
        }

        // GET: api/Editoriales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Editorial>> GetEditorial(int id)
        {
            var editorial = await _context.Editoriales.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if (editorial == null)
            {
                return NotFound();
            }

            return editorial;
        }

        // PUT: api/Editoriales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditorial(int id, Editorial editorial)
        {
            if (id != editorial.Id)
            {
                return BadRequest();
            }

            _context.Entry(editorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditorialExists(id))
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

        // POST: api/Editoriales
        [HttpPost]
        public async Task<ActionResult<Editorial>> PostEditorial(Editorial editorial)
        {
            _context.Editoriales.Add(editorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEditorial", new { id = editorial.Id }, editorial);
        }

        // DELETE: api/Editoriales/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditorial(int id)
        {
            var editorial = await _context.Editoriales.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }

            editorial.IsDeleted = true;
            _context.Editoriales.Update(editorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Editoriales/restore/5
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreEditorial(int id)
        {
            var editorial = await _context.Editoriales.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (editorial == null)
            {
                return NotFound();
            }
            editorial.IsDeleted = false;
            _context.Editoriales.Update(editorial);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EditorialExists(int id)
        {
            return _context.Editoriales.Any(e => e.Id == id);
        }
    }
}
