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
    public class TutoresMascotaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TutoresMascotaController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TutoresMascota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorMascota>>> GetTutoresMascota()
        {
            return await _context.TutoresMascota.ToListAsync();
        }

        // GET: api/TutoresMascota/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TutorMascota>> GetTutorMascota(int id)
        {
            var tutorMascota = await _context.TutoresMascota.FindAsync(id);

            if (tutorMascota == null)
            {
                return NotFound();
            }

            return tutorMascota;
        }

        // PUT: api/TutoresMascota/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorMascota(int id, TutorMascota tutorMascota)
        {
            if (id != tutorMascota.idTutorMascota)
            {
                return BadRequest();
            }

            _context.Entry(tutorMascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorMascotaExists(id))
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

        // POST: api/TutoresMascota
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TutorMascota>> PostTutorMascota(TutorMascota tutorMascota)
        {
            _context.TutoresMascota.Add(tutorMascota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutorMascota", new { id = tutorMascota.idTutorMascota }, tutorMascota);
        }

        // DELETE: api/TutoresMascota/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTutorMascota(int id)
        //{
        //    var tutorMascota = await _context.TutoresMascota.FindAsync(id);
        //    if (tutorMascota == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TutoresMascota.Remove(tutorMascota);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TutorMascotaExists(int id)
        {
            return _context.TutoresMascota.Any(e => e.idTutorMascota == id);
        }
    }
}
