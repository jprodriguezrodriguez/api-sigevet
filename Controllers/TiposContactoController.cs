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
    public class TiposContactoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposContactoController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposContacto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContacto>>> GetTiposContacto()
        {
            return await _context.TiposContacto.ToListAsync();
        }

        // GET: api/TiposContacto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoContacto>> GetTipoContacto(int id)
        {
            var tipoContacto = await _context.TiposContacto.FindAsync(id);

            if (tipoContacto == null)
            {
                return NotFound();
            }

            return tipoContacto;
        }

        // PUT: api/TiposContacto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoContacto(int id, TipoContacto tipoContacto)
        {
            if (id != tipoContacto.idTipoContacto)
            {
                return BadRequest();
            }

            _context.Entry(tipoContacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoContactoExists(id))
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

        // POST: api/TiposContacto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoContacto>> PostTipoContacto(TipoContacto tipoContacto)
        {
            _context.TiposContacto.Add(tipoContacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoContacto", new { id = tipoContacto.idTipoContacto }, tipoContacto);
        }

        // DELETE: api/TiposContacto/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTipoContacto(int id)
        //{
        //    var tipoContacto = await _context.TiposContacto.FindAsync(id);
        //    if (tipoContacto == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TiposContacto.Remove(tipoContacto);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TipoContactoExists(int id)
        {
            return _context.TiposContacto.Any(e => e.idTipoContacto == id);
        }
    }
}
