using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacunacionesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public VacunacionesController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Vacunaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacunacion>>> GetVacunaciones()
        {
            return await _context.Vacunaciones.ToListAsync();
        }

        // GET: api/Vacunaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacunacion>> GetVacunacion(int id)
        {
            var vacunacion = await _context.Vacunaciones.FindAsync(id);

            if (vacunacion == null)
            {
                return NotFound();
            }

            return vacunacion;
        }

        // PUT: api/Vacunaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacunacion(int id, Vacunacion vacunacion)
        {
            if (id != vacunacion.idVacunacion)
            {
                return BadRequest();
            }

            _context.Entry(vacunacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacunacionExists(id))
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

        // POST: api/Vacunaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vacunacion>> PostVacunacion(Vacunacion vacunacion)
        {
            _context.Vacunaciones.Add(vacunacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacunacion", new { id = vacunacion.idVacunacion }, vacunacion);
        }

        // DELETE: api/Vacunaciones/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVacunacion(int id)
        //{
        //    var vacunacion = await _context.Vacunaciones.FindAsync(id);
        //    if (vacunacion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Vacunaciones.Remove(vacunacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool VacunacionExists(int id)
        {
            return _context.Vacunaciones.Any(e => e.idVacunacion == id);
        }
    }
}
