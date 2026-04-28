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
    public class TutoresController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TutoresController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Tutores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetTutores()
        {
            return await _context.Tutores
                .Include(t => t.persona)
                .Include(t => t.estadoCuenta)
                .ToListAsync();
        }

        // GET: api/Tutores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetTutor(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);

            if (tutor == null)
            {
                return NotFound();
            }

            return tutor;
        }

        // PUT: api/Tutores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutor(int id, Tutor tutor)
        {
            if (id != tutor.idPersonaTut)
            {
                return BadRequest();
            }

            _context.Entry(tutor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorExists(id))
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

        // POST: api/Tutores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tutor>> PostTutor(Tutor tutor)
        {
            _context.Tutores.Add(tutor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TutorExists(tutor.idPersonaTut))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTutor", new { id = tutor.idPersonaTut }, tutor);
        }

        // DELETE: api/Tutores/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTutor(int id)
        //{
        //    var tutor = await _context.Tutores.FindAsync(id);
        //    if (tutor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tutores.Remove(tutor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TutorExists(int id)
        {
            return _context.Tutores.Any(e => e.idPersonaTut == id);
        }
    }
}
