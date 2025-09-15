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
    public class EditorialesController : ControllerBase
    {
        private readonly BiblioContext _context;

        public EditorialesController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditoriales([FromQuery] string filtro = "")
        {
            return await _context.Editoriales.AsNoTracking().Where(e => e.Nombre.Contains(filtro)).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetDeletedsEditoriales([FromQuery] string filtro = "")
        {
            return await _context.Editoriales.AsNoTracking().IgnoreQueryFilters().Where(e => e.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Editorial>> GetEditorial(int id)
        {
            var editorial = await _context.Editoriales.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (editorial == null)
            {
                return NotFound();
            }
            return editorial;
        }

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

        [HttpPost]
        public async Task<ActionResult<Editorial>> PostEditorial(Editorial editorial)
        {
            _context.Editoriales.Add(editorial);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEditorial", new { id = editorial.Id }, editorial);
        }

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

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreEditorial(int id)
        {
            var editorial = await _context.Editoriales.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id.Equals(id));
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
