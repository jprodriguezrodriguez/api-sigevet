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
    public class TiposAlertaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposAlertaController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposAlerta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAlerta>>> GetTiposAlerta()
        {
            return await _context.TiposAlerta.ToListAsync();
        }

        // GET: api/TiposAlerta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoAlerta>> GetTipoAlerta(int id)
        {
            var tipoAlerta = await _context.TiposAlerta.FindAsync(id);

            if (tipoAlerta == null)
            {
                return NotFound();
            }

            return tipoAlerta;
        }

        // PUT: api/TiposAlerta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAlerta(int id, TipoAlerta tipoAlerta)
        {
            if (id != tipoAlerta.idTipoAlerta)
            {
                return BadRequest();
            }

            _context.Entry(tipoAlerta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoAlertaExists(id))
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

        // POST: api/TiposAlerta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoAlerta>> PostTipoAlerta(TipoAlerta tipoAlerta)
        {
            _context.TiposAlerta.Add(tipoAlerta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoAlerta", new { id = tipoAlerta.idTipoAlerta }, tipoAlerta);
        }

        // DELETE: api/TiposAlerta/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoAlerta(int id)
        //{
        //    var tipoAlerta = await _context.TiposAlerta.FindAsync(id);
        //    if (tipoAlerta == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TiposAlerta.Remove(tipoAlerta);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TipoAlertaExists(int id)
        {
            return _context.TiposAlerta.Any(e => e.idTipoAlerta == id);
        }
    }
}
