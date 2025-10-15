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
    public class UsuariosController : ControllerBase
    {
        private readonly BiblioContext _context;

        public UsuariosController(BiblioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios([FromQuery] string filtro = "")
        {
            return await _context.Usuario.AsNoTracking().Where(u => u.Nombre.Contains(filtro)).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetDeletedsUsuarios([FromQuery] string filtro = "")
        {
            return await _context.Usuario.AsNoTracking().IgnoreQueryFilters().Where(u => u.IsDeleted).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpGet("byemail")]
        public async Task<ActionResult<Usuario>> GetByEmailUsuario([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("El parametro email es obligatorio.");
            }
            var usuario = await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }
            _context.Entry(usuario).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Verificar si ya existe un usuario con ese email
            if (await _context.Usuario.AnyAsync(u => u.Email == usuario.Email))
            {
                return Conflict("Ya existe un usuario con ese email");
            }

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.IsDeleted = true;
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreUsuario(int id)
        {
            var usuario = await _context.Usuario.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.IsDeleted = false;
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
