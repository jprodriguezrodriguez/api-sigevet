using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Veterinarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public VeterinariosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeterinariosResponseDto>>> GetVeterinarios()
        {
            var veterinarios = await _context.Veterinarios
                .Include(v => v.persona)
                .Include(v => v.estadoVeterinario)
                .Where(v => v.persona == null || !v.persona.isDeleted)
                .Select(v => new VeterinariosResponseDto
                {
                    idPersonaVet = v.idPersonaVet,
                    numeroTarjetaProfesional = v.numeroTarjetaProfesional,
                    fechaRegistroVeterinario = v.fechaRegistroVeterinario,
                    fechaActualizacionVeterinario = v.fechaActualizacionVeterinario,
                    persona = v.persona != null ? v.persona.primerNombre + " " + v.persona.primerApellido : null,
                    estadoVeterinario = v.estadoVeterinario != null ? v.estadoVeterinario.estado : null
                }).ToListAsync();

            return Ok(veterinarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VeterinariosResponseDto>> GetVeterinario(int id)
        {
            var veterinario = await _context.Veterinarios
                .Include(v => v.persona)
                .Include(v => v.estadoVeterinario)
                .Where(v => v.idPersonaVet == id && (v.persona == null || !v.persona.isDeleted))
                .Select(v => new VeterinariosResponseDto
                {
                    idPersonaVet = v.idPersonaVet,
                    numeroTarjetaProfesional = v.numeroTarjetaProfesional,
                    fechaRegistroVeterinario = v.fechaRegistroVeterinario,
                    fechaActualizacionVeterinario = v.fechaActualizacionVeterinario,
                    persona = v.persona != null ? v.persona.primerNombre + " " + v.persona.primerApellido : null,
                    estadoVeterinario = v.estadoVeterinario != null ? v.estadoVeterinario.estado : null
                }).FirstOrDefaultAsync();

            if (veterinario == null)
            {
                return NotFound();
            }

            return Ok(veterinario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeterinario(int id, [FromForm] VeterinariosFormDto veterinarioDto)
        {
            if (id != veterinarioDto.idPersonaVet)
            {
                return BadRequest();
            }

            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }

            veterinario.numeroTarjetaProfesional = veterinarioDto.numeroTarjetaProfesional;
            veterinario.idEstadoDisponibilidad = veterinarioDto.idEstadoDisponibilidad;
            veterinario.fechaActualizacionVeterinario = DateTime.Now;

            _context.Veterinarios.Update(veterinario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VeterinariosResponseDto>> PostVeterinario([FromForm] VeterinariosFormDto veterinarioDto)
        {
            var veterinario = new Veterinario
            {
                idPersonaVet = veterinarioDto.idPersonaVet,
                numeroTarjetaProfesional = veterinarioDto.numeroTarjetaProfesional,
                idEstadoDisponibilidad = veterinarioDto.idEstadoDisponibilidad
            };

            _context.Veterinarios.Add(veterinario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VeterinarioExists(veterinario.idPersonaVet))
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction(nameof(GetVeterinario), new { id = veterinario.idPersonaVet }, veterinario);
        }

        private bool VeterinarioExists(int id)
        {
            return _context.Veterinarios.Any(e => e.idPersonaVet == id);
        }
    }
}
