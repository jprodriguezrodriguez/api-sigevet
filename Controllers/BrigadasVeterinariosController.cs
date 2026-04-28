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
    public class BrigadasVeterinariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public BrigadasVeterinariosController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/BrigadasVeterinarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrigadaVeterinario>>> GetBrigadasVeterinario()
        {
            return await _context.BrigadasVeterinario.ToListAsync();
        }

        // GET: api/BrigadasVeterinarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrigadaVeterinario>> GetBrigadaVeterinario(int id)
        {
            var brigadaVeterinario = await _context.BrigadasVeterinario.FindAsync(id);

            if (brigadaVeterinario == null)
            {
                return NotFound();
            }

            return brigadaVeterinario;
        }

        // PUT: api/BrigadasVeterinarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrigadaVeterinario(int id, BrigadaVeterinario brigadaVeterinario)
        {
            if (id != brigadaVeterinario.idBrigadaVeterinario)
            {
                return BadRequest();
            }

            _context.Entry(brigadaVeterinario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrigadaVeterinarioExists(id))
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

        // POST: api/BrigadasVeterinarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrigadaVeterinario>> PostBrigadaVeterinario(BrigadaVeterinario brigadaVeterinario)
        {
            _context.BrigadasVeterinario.Add(brigadaVeterinario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrigadaVeterinario", new { id = brigadaVeterinario.idBrigadaVeterinario }, brigadaVeterinario);
        }

        // DELETE: api/BrigadasVeterinarios/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBrigadaVeterinario(int id)
        //{
        //    var brigadaVeterinario = await _context.BrigadasVeterinario.FindAsync(id);
        //    if (brigadaVeterinario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.BrigadasVeterinario.Remove(brigadaVeterinario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool BrigadaVeterinarioExists(int id)
        {
            return _context.BrigadasVeterinario.Any(e => e.idBrigadaVeterinario == id);
        }
    }
}
