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
    public class VacunacionesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public VacunacionesController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Vacunaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacunacion>>> GetVacunaciones()
        {
            return await _context.Vacunaciones.ToListAsync();
        }

        // GET: api/Vacunaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacunacion>> GetVacunacion(int id)
        {
            var vacunacion = await _context.Vacunaciones.FindAsync(id);

            if (vacunacion == null)
            {
                return NotFound();
            }

            return vacunacion;
        }

        // PUT: api/Vacunaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacunacion(int id, Vacunacion vacunacion)
        {
            if (id != vacunacion.idVacunacion)
            {
                return BadRequest();
            }

            _context.Entry(vacunacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacunacionExists(id))
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

        // POST: api/Vacunaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostVacunacion(Vacunacion vacunacion)
        {
            var esquema = await _context.EsquemasVacunacion
                .FirstOrDefaultAsync(e => e.idEsquemaVacunacion == vacunacion.idEsquemaVacunacion);

            if (esquema == null)
            {
                return NotFound($"No existe un esquema de vacunación con ID {vacunacion.idEsquemaVacunacion}.");
            }

            var mascotaExiste = await _context.Mascotas
                .AnyAsync(m => m.idMascota == vacunacion.idMascota);

            if (!mascotaExiste)
            {
                return NotFound($"No existe una mascota con ID {vacunacion.idMascota}.");
            }

            var unidadExiste = await _context.UnidadesMedida
                .AnyAsync(u => u.idUnidadMedida == vacunacion.idUnidadMedida);

            if (!unidadExiste)
            {
                return NotFound($"No existe una unidad de medida con ID {vacunacion.idUnidadMedida}.");
            }

            vacunacion.CalcularProximaFecha(esquema.intervaloDias);

            vacunacion.fechaCreacion = DateTime.Now;
            vacunacion.fechaActualizacion = DateTime.Now;

            _context.Vacunaciones.Add(vacunacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetVacunacion),
                new { id = vacunacion.idVacunacion },
                new
                {
                    vacunacion.idVacunacion,
                    vacunacion.fechaAplicacion,
                    vacunacion.dosisAplicada,
                    vacunacion.numeroDosis,
                    vacunacion.observaciones,
                    vacunacion.proximaFecha,
                    vacunacion.idEsquemaVacunacion,
                    vacunacion.idUnidadMedida,
                    vacunacion.idMascota
                }
            );
        }

        // DELETE: api/Vacunaciones/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVacunacion(int id)
        //{
        //    var vacunacion = await _context.Vacunaciones.FindAsync(id);
        //    if (vacunacion == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Vacunaciones.Remove(vacunacion);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool VacunacionExists(int id)
        {
            return _context.Vacunaciones.Any(e => e.idVacunacion == id);
        }
    }
}
