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
    public class TiposMovimientoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposMovimientoController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposMovimiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMovimiento>>> GetTiposMovimiento()
        {
            return await _context.TiposMovimiento.ToListAsync();
        }

        // GET: api/TiposMovimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMovimiento>> GetTipoMovimiento(int id)
        {
            var tipoMovimiento = await _context.TiposMovimiento.FindAsync(id);

            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            return tipoMovimiento;
        }

        // PUT: api/TiposMovimiento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMovimiento(int id, TipoMovimiento tipoMovimiento)
        {
            if (id != tipoMovimiento.idTipoMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(tipoMovimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMovimientoExists(id))
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

        // POST: api/TiposMovimiento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoMovimiento>> PostTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            _context.TiposMovimiento.Add(tipoMovimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoMovimiento", new { id = tipoMovimiento.idTipoMovimiento }, tipoMovimiento);
        }

        // DELETE: api/TiposMovimiento/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoMovimiento(int id)
        //{
        //    var tipoMovimiento = await _context.TiposMovimiento.FindAsync(id);
        //    if (tipoMovimiento == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TiposMovimiento.Remove(tipoMovimiento);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TipoMovimientoExists(int id)
        {
            return _context.TiposMovimiento.Any(e => e.idTipoMovimiento == id);
        }
    }
}
