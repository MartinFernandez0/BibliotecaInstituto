using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarrerasController : ControllerBase
    {
        private readonly BiblioContext _context;

        public CarrerasController(BiblioContext context)
        {
            _context = context;
        }

        // GET: api/Carreras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrera>>> GetCarreras([FromQuery] string filtro = "")
        {
            return await _context.Carreras.AsNoTracking().Where(c => c.Nombre.Contains(filtro)).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Carrera>>> GetDeletedsCarreras([FromQuery] string filtro = "")
        {
            return await _context.Carreras.AsNoTracking().IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync();
        }

        // GET: api/Carreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(int id)
        {
            var carrera = await _context.Carreras.AsNoTracking().FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (carrera == null)
            {
                return NotFound();
            }

            return carrera;
        }

        // PUT: api/Carreras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrera(int id, Carrera carrera)
        {
            if (id != carrera.Id)
            {
                return BadRequest();
            }

            _context.Entry(carrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
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

        // POST: api/Carreras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrera>> PostCarrera(Carrera carrera)
        {
            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrera", new { id = carrera.Id }, carrera);
        }

        // DELETE: api/Carreras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            carrera.IsDeleted = true;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreCarrera(int id)
        {
            var carrera = await _context.Carreras.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (carrera == null)
            {
                return NotFound();
            }
            carrera.IsDeleted = false;
            _context.Carreras.Update(carrera);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.Id == id);
        }
    }
}
