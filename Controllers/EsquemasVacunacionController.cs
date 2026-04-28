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
    public class EsquemasVacunacionController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EsquemasVacunacionController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/EsquemasVacunacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EsquemaVacunacion>>> GetEsquemasVacunacion()
        {
            return await _context.EsquemasVacunacion.ToListAsync();
        }

        // GET: api/EsquemasVacunacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EsquemaVacunacion>> GetEsquemaVacunacion(int id)
        {
            var esquemaVacunacion = await _context.EsquemasVacunacion.FindAsync(id);

            if (esquemaVacunacion == null)
            {
                return NotFound();
            }

            return esquemaVacunacion;
        }

        // PUT: api/EsquemasVacunacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEsquemaVacunacion(int id, EsquemaVacunacion esquemaVacunacion)
        {
            if (id != esquemaVacunacion.idEsquemaVacunacion)
            {
                return BadRequest();
            }

            _context.Entry(esquemaVacunacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EsquemaVacunacionExists(id))
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

        // POST: api/EsquemasVacunacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EsquemaVacunacion>> PostEsquemaVacunacion(EsquemaVacunacion esquemaVacunacion)
        {
            _context.EsquemasVacunacion.Add(esquemaVacunacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEsquemaVacunacion", new { id = esquemaVacunacion.idEsquemaVacunacion }, esquemaVacunacion);
        }

        // DELETE: api/EsquemasVacunacion/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEsquemaVacunacion(int id)
        //{
        //    var esquemaVacunacion = await _context.EsquemasVacunacion.FindAsync(id);
        //    if (esquemaVacunacion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.EsquemasVacunacion.Remove(esquemaVacunacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool EsquemaVacunacionExists(int id)
        {
            return _context.EsquemasVacunacion.Any(e => e.idEsquemaVacunacion == id);
        }
    }
}
