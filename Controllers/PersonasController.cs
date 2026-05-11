using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Personas;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public PersonasController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasResponseDto>>> GetPersonas()
        {
            var personas = await _context.Personas
                .Include(p => p.tipoIdentificacion)
                .Include(p => p.estadoPersona)
                .Where(p => !p.isDeleted)
                .Select(p => new PersonasResponseDto
                {
                    idPersona = p.idPersona,
                    primerNombre = p.primerNombre,
                    segundoNombre = p.segundoNombre,
                    primerApellido = p.primerApellido,
                    segundoApellido = p.segundoApellido,
                    numeroIdentificacion = p.numeroIdentificacion,
                    fechaNacimiento = p.fechaNacimiento,
                    direccion = p.direccion,
                    tipoIdentificacion = p.tipoIdentificacion != null ? p.tipoIdentificacion.tipoIdentificacion : null,
                    estadoPersona = p.estadoPersona != null ? p.estadoPersona.estado : null
                }).ToListAsync();

            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonasResponseDto>> GetPersona(int id)
        {
            var persona = await _context.Personas
                .Include(p => p.tipoIdentificacion)
                .Include(p => p.estadoPersona)
                .Where(p => !p.isDeleted && p.idPersona == id)
                .Select(p => new PersonasResponseDto
                {
                    idPersona = p.idPersona,
                    primerNombre = p.primerNombre,
                    segundoNombre = p.segundoNombre,
                    primerApellido = p.primerApellido,
                    segundoApellido = p.segundoApellido,
                    numeroIdentificacion = p.numeroIdentificacion,
                    fechaNacimiento = p.fechaNacimiento,
                    direccion = p.direccion,
                    tipoIdentificacion = p.tipoIdentificacion != null ? p.tipoIdentificacion.tipoIdentificacion : null,
                    estadoPersona = p.estadoPersona != null ? p.estadoPersona.estado : null
                }).FirstOrDefaultAsync();

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, PersonasFormDto personaDto)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null || persona.isDeleted)
            {
                return NotFound();
            }

            persona.primerNombre = personaDto.primerNombre;
            persona.segundoNombre = personaDto.segundoNombre;
            persona.primerApellido = personaDto.primerApellido;
            persona.segundoApellido = personaDto.segundoApellido;
            persona.numeroIdentificacion = personaDto.numeroIdentificacion;
            persona.fechaNacimiento = personaDto.fechaNacimiento;
            persona.direccion = personaDto.direccion;
            persona.idTipoIdentificacion = personaDto.idTipoIdentificacion;
            persona.idEstadoPersona = personaDto.idEstadoPersona;

            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PersonasResponseDto>> PostPersona(PersonasFormDto personaDto)
        {
            var persona = new Persona
            {
                primerNombre = personaDto.primerNombre,
                segundoNombre = personaDto.segundoNombre,
                primerApellido = personaDto.primerApellido,
                segundoApellido = personaDto.segundoApellido,
                numeroIdentificacion = personaDto.numeroIdentificacion,
                fechaNacimiento = personaDto.fechaNacimiento,
                direccion = personaDto.direccion,
                idTipoIdentificacion = personaDto.idTipoIdentificacion,
                idEstadoPersona = personaDto.idEstadoPersona
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.idPersona }, persona);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null || persona.isDeleted)
            {
                return NotFound();
            }

            persona.isDeleted = true;
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.idPersona == id);
        }
    }
}
