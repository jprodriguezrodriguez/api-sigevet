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
    public class InsumosSanitariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public InsumosSanitariosController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/InsumosSanitarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsumoSanitario>>> GetInsumosSanitarios()
        {
            return await _context.InsumosSanitarios.ToListAsync();
        }

        // GET: api/InsumosSanitarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsumoSanitario>> GetInsumoSanitario(int id)
        {
            var insumoSanitario = await _context.InsumosSanitarios.FindAsync(id);

            if (insumoSanitario == null)
            {
                return NotFound();
            }

            return insumoSanitario;
        }

        // PUT: api/InsumosSanitarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumoSanitario(int id, InsumoSanitario insumoSanitario)
        {
            if (id != insumoSanitario.idInsumoSanitario)
            {
                return BadRequest();
            }

            _context.Entry(insumoSanitario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsumoSanitarioExists(id))
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

        // POST: api/InsumosSanitarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsumoSanitario>> PostInsumoSanitario(InsumoSanitario insumoSanitario)
        {
            _context.InsumosSanitarios.Add(insumoSanitario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsumoSanitario", new { id = insumoSanitario.idInsumoSanitario }, insumoSanitario);
        }

        // DELETE: api/InsumosSanitarios/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteInsumoSanitario(int id)
        //{
        //    var insumoSanitario = await _context.InsumosSanitarios.FindAsync(id);
        //    if (insumoSanitario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.InsumosSanitarios.Remove(insumoSanitario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool InsumoSanitarioExists(int id)
        {
            return _context.InsumosSanitarios.Any(e => e.idInsumoSanitario == id);
        }
    }
}
