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
    public class RolesParticipacionController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public RolesParticipacionController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/RolesParticipacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolParticipacion>>> GetRolesParticipacion()
        {
            return await _context.RolesParticipacion.ToListAsync();
        }

        // GET: api/RolesParticipacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolParticipacion>> GetRolParticipacion(int id)
        {
            var rolParticipacion = await _context.RolesParticipacion.FindAsync(id);

            if (rolParticipacion == null)
            {
                return NotFound();
            }

            return rolParticipacion;
        }

        // PUT: api/RolesParticipacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolParticipacion(int id, RolParticipacion rolParticipacion)
        {
            if (id != rolParticipacion.idRolParticipacion)
            {
                return BadRequest();
            }

            _context.Entry(rolParticipacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolParticipacionExists(id))
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

        // POST: api/RolesParticipacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RolParticipacion>> PostRolParticipacion(RolParticipacion rolParticipacion)
        {
            _context.RolesParticipacion.Add(rolParticipacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolParticipacion", new { id = rolParticipacion.idRolParticipacion }, rolParticipacion);
        }

        // DELETE: api/RolesParticipacion/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRolParticipacion(int id)
        //{
        //    var rolParticipacion = await _context.RolesParticipacion.FindAsync(id);
        //    if (rolParticipacion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.RolesParticipacion.Remove(rolParticipacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool RolParticipacionExists(int id)
        {
            return _context.RolesParticipacion.Any(e => e.idRolParticipacion == id);
        }
    }
}
