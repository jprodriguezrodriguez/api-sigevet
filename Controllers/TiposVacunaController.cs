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
    public class TiposVacunaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposVacunaController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposVacuna
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoVacuna>>> GetTiposVacuna()
        {
            return await _context.TiposVacuna.ToListAsync();
        }

        // GET: api/TiposVacuna/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoVacuna>> GetTipoVacuna(int id)
        {
            var tipoVacuna = await _context.TiposVacuna.FindAsync(id);

            if (tipoVacuna == null)
            {
                return NotFound();
            }

            return tipoVacuna;
        }

        // PUT: api/TiposVacuna/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoVacuna(int id, TipoVacuna tipoVacuna)
        {
            if (id != tipoVacuna.idTipoVacuna)
            {
                return BadRequest();
            }

            _context.Entry(tipoVacuna).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoVacunaExists(id))
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

        // POST: api/TiposVacuna
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoVacuna>> PostTipoVacuna(TipoVacuna tipoVacuna)
        {
            _context.TiposVacuna.Add(tipoVacuna);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoVacuna", new { id = tipoVacuna.idTipoVacuna }, tipoVacuna);
        }

        // DELETE: api/TiposVacuna/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoVacuna(int id)
        //{
        //    var tipoVacuna = await _context.TiposVacuna.FindAsync(id);
        //    if (tipoVacuna == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TiposVacuna.Remove(tipoVacuna);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TipoVacunaExists(int id)
        {
            return _context.TiposVacuna.Any(e => e.idTipoVacuna == id);
        }
    }
}
