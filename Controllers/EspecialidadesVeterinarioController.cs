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
    public class EspecialidadesVeterinarioController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspecialidadesVeterinarioController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/EspecialidadesVeterinario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadVeterinario>>> GetEspecialidadesVeterinario()
        {
            return await _context.EspecialidadesVeterinario.ToListAsync();
        }

        // GET: api/EspecialidadesVeterinario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadVeterinario>> GetEspecialidadVeterinario(int id)
        {
            var especialidadVeterinario = await _context.EspecialidadesVeterinario.FindAsync(id);

            if (especialidadVeterinario == null)
            {
                return NotFound();
            }

            return especialidadVeterinario;
        }

        // PUT: api/EspecialidadesVeterinario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidadVeterinario(int id, EspecialidadVeterinario especialidadVeterinario)
        {
            if (id != especialidadVeterinario.idVeterinarioEspecialidad)
            {
                return BadRequest();
            }

            _context.Entry(especialidadVeterinario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadVeterinarioExists(id))
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

        // POST: api/EspecialidadesVeterinario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EspecialidadVeterinario>> PostEspecialidadVeterinario(EspecialidadVeterinario especialidadVeterinario)
        {
            _context.EspecialidadesVeterinario.Add(especialidadVeterinario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecialidadVeterinario", new { id = especialidadVeterinario.idVeterinarioEspecialidad }, especialidadVeterinario);
        }

        // DELETE: api/EspecialidadesVeterinario/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEspecialidadVeterinario(int id)
        //{
        //    var especialidadVeterinario = await _context.EspecialidadesVeterinario.FindAsync(id);
        //    if (especialidadVeterinario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.EspecialidadesVeterinario.Remove(especialidadVeterinario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool EspecialidadVeterinarioExists(int id)
        {
            return _context.EspecialidadesVeterinario.Any(e => e.idVeterinarioEspecialidad == id);
        }
    }
}
