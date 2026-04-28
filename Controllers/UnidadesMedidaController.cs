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
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public UnidadesMedidaController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/UnidadesMedida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedida>>> GetUnidadesMedida()
        {
            return await _context.UnidadesMedida.ToListAsync();
        }

        // GET: api/UnidadesMedida/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedida>> GetUnidadMedida(int id)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);

            if (unidadMedida == null)
            {
                return NotFound();
            }

            return unidadMedida;
        }

        // PUT: api/UnidadesMedida/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadMedida(int id, UnidadMedida unidadMedida)
        {
            if (id != unidadMedida.idUnidadMedida)
            {
                return BadRequest();
            }

            _context.Entry(unidadMedida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadMedidaExists(id))
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

        // POST: api/UnidadesMedida
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnidadMedida>> PostUnidadMedida(UnidadMedida unidadMedida)
        {
            _context.UnidadesMedida.Add(unidadMedida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnidadMedida", new { id = unidadMedida.idUnidadMedida }, unidadMedida);
        }

        // DELETE: api/UnidadesMedida/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUnidadMedida(int id)
        //{
        //    var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
        //    if (unidadMedida == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UnidadesMedida.Remove(unidadMedida);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UnidadMedidaExists(int id)
        {
            return _context.UnidadesMedida.Any(e => e.idUnidadMedida == id);
        }
    }
}
