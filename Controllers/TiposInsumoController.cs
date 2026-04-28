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
    public class TiposInsumoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposInsumoController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposInsumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoInsumo>>> GetTiposInsumo()
        {
            return await _context.TiposInsumo.ToListAsync();
        }

        // GET: api/TiposInsumo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoInsumo>> GetTipoInsumo(int id)
        {
            var tipoInsumo = await _context.TiposInsumo.FindAsync(id);

            if (tipoInsumo == null)
            {
                return NotFound();
            }

            return tipoInsumo;
        }

        // PUT: api/TiposInsumo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoInsumo(int id, TipoInsumo tipoInsumo)
        {
            if (id != tipoInsumo.idTipoInsumo)
            {
                return BadRequest();
            }

            _context.Entry(tipoInsumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoInsumoExists(id))
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

        // POST: api/TiposInsumo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoInsumo>> PostTipoInsumo(TipoInsumo tipoInsumo)
        {
            _context.TiposInsumo.Add(tipoInsumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoInsumo", new { id = tipoInsumo.idTipoInsumo }, tipoInsumo);
        }

        // DELETE: api/TiposInsumo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoInsumo(int id)
        //{
        //    var tipoInsumo = await _context.TiposInsumo.FindAsync(id);
        //    if (tipoInsumo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TiposInsumo.Remove(tipoInsumo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TipoInsumoExists(int id)
        {
            return _context.TiposInsumo.Any(e => e.idTipoInsumo == id);
        }
    }
}
