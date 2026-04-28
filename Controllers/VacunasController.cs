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
    public class VacunasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public VacunasController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Vacunas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacuna>>> GetVacunas()
        {
            return await _context.Vacunas.ToListAsync();
        }

        // GET: api/Vacunas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacuna>> GetVacuna(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);

            if (vacuna == null)
            {
                return NotFound();
            }

            return vacuna;
        }

        // PUT: api/Vacunas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacuna(int id, Vacuna vacuna)
        {
            if (id != vacuna.idVacuna)
            {
                return BadRequest();
            }

            _context.Entry(vacuna).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacunaExists(id))
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

        // POST: api/Vacunas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vacuna>> PostVacuna(Vacuna vacuna)
        {
            _context.Vacunas.Add(vacuna);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacuna", new { id = vacuna.idVacuna }, vacuna);
        }

        // DELETE: api/Vacunas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVacuna(int id)
        //{
        //    var vacuna = await _context.Vacunas.FindAsync(id);
        //    if (vacuna == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Vacunas.Remove(vacuna);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool VacunaExists(int id)
        {
            return _context.Vacunas.Any(e => e.idVacuna == id);
        }
    }
}
