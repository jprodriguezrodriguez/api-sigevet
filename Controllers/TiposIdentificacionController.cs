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
    public class TiposIdentificacionController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposIdentificacionController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoIdentificacion>>> GetTiposIdentificacion()
        {
            return await _context.TiposIdentificacion.ToListAsync();
        }

        // GET: api/TipoIdentificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIdentificacion>> GetTipoIdentificacion(int id)
        {
            var tipoIdentificacion = await _context.TiposIdentificacion.FindAsync(id);

            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            return tipoIdentificacion;
        }

        // PUT: api/TipoIdentificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoIdentificacion(int id, TipoIdentificacion tipoIdentificacion)
        {
            if (id != tipoIdentificacion.idTipoIdentificacion)
            {
                return BadRequest();
            }

            _context.Entry(tipoIdentificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoIdentificacionExists(id))
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

        // POST: api/TipoIdentificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoIdentificacion>> PostTipoIdentificacion(TipoIdentificacion tipoIdentificacion)
        {
            _context.TiposIdentificacion.Add(tipoIdentificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoIdentificacion", new { id = tipoIdentificacion.idTipoIdentificacion }, tipoIdentificacion);
        }

        // DELETE: api/TipoIdentificacions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoIdentificacion(int id)
        //{
        //    var tipoIdentificacion = await _context.TiposIdentificacion.FindAsync(id);
        //    if (tipoIdentificacion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TiposIdentificacion.Remove(tipoIdentificacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TipoIdentificacionExists(int id)
        {
            return _context.TiposIdentificacion.Any(e => e.idTipoIdentificacion == id);
        }
    }
}
