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
    public class BrigadasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public BrigadasController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Brigadas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brigada>>> GetBrigadas()
        {
            return await _context.Brigadas.ToListAsync();
        }

        // GET: api/Brigadas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brigada>> GetBrigada(int id)
        {
            var brigada = await _context.Brigadas.FindAsync(id);

            if (brigada == null)
            {
                return NotFound();
            }

            return brigada;
        }

        // PUT: api/Brigadas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrigada(int id, Brigada brigada)
        {
            if (id != brigada.idBrigada)
            {
                return BadRequest();
            }

            _context.Entry(brigada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrigadaExists(id))
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

        // POST: api/Brigadas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brigada>> PostBrigada(Brigada brigada)
        {
            _context.Brigadas.Add(brigada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrigada", new { id = brigada.idBrigada }, brigada);
        }

        // DELETE: api/Brigadas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBrigada(int id)
        //{
        //    var brigada = await _context.Brigadas.FindAsync(id);
        //    if (brigada == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Brigadas.Remove(brigada);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool BrigadaExists(int id)
        {
            return _context.Brigadas.Any(e => e.idBrigada == id);
        }
    }
}
