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
    public class UsuarioCarrerasController : ControllerBase
    {
        private readonly BiblioContext _context;

        public UsuarioCarrerasController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCarrera>>> GetUsuarioCarreras([FromQuery] string filtro = "")
        {
            return await _context.UsuarioCarreras.Include(uc => uc.Usuario).Include(uc => uc.Carrera).AsNoTracking().Where(uc => uc.Usuario.Nombre.Contains(filtro.ToUpper()) || uc.Carrera.Nombre.Contains(filtro.ToUpper())).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<UsuarioCarrera>>> GetDeletedsUsuarioCarreras()
        {
            return await _context.UsuarioCarreras.AsNoTracking().IgnoreQueryFilters().Where(uc => uc.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCarrera>> GetUsuarioCarrera(int id)
        {
            var usuarioCarrera = await _context.UsuarioCarreras.AsNoTracking().FirstOrDefaultAsync(uc => uc.Id.Equals(id));
            if (usuarioCarrera == null)
            {
                return NotFound();
            }
            return usuarioCarrera;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioCarrera(int id, UsuarioCarrera usuarioCarrera)
        {
            _context.TryAttach(usuarioCarrera?.Usuario);
            _context.TryAttach(usuarioCarrera?.Carrera);
            if (id != usuarioCarrera.Id)
            {
                return BadRequest();
            }
            _context.Entry(usuarioCarrera).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioCarreraExists(id))
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
        public async Task<ActionResult<UsuarioCarrera>> PostUsuarioCarrera(UsuarioCarrera usuarioCarrera)
        {
            _context.TryAttach(usuarioCarrera?.Usuario);
            _context.TryAttach(usuarioCarrera?.Carrera);
            _context.UsuarioCarreras.Add(usuarioCarrera);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsuarioCarrera", new { id = usuarioCarrera.Id }, usuarioCarrera);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioCarrera(int id)
        {
            var usuarioCarrera = await _context.UsuarioCarreras.FindAsync(id);
            if (usuarioCarrera == null)
            {
                return NotFound();
            }
            usuarioCarrera.IsDeleted = true;
            _context.UsuarioCarreras.Update(usuarioCarrera);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreUsuarioCarrera(int id)
        {
            var usuarioCarrera = await _context.UsuarioCarreras.IgnoreQueryFilters().FirstOrDefaultAsync(uc => uc.Id.Equals(id));
            if (usuarioCarrera == null)
            {
                return NotFound();
            }
            usuarioCarrera.IsDeleted = false;
            _context.UsuarioCarreras.Update(usuarioCarrera);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioCarreraExists(int id)
        {
            return _context.UsuarioCarreras.Any(e => e.Id == id);
        }
    }
}
