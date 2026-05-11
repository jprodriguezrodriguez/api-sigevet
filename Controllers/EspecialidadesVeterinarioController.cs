using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.EspecialidadesVeterinarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesVeterinariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspecialidadesVeterinariosController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/EspecialidadesVeterinarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadVeterinarioResponseDto>>> GetEspecialidadesVeterinarios()
        {
            var especialidadesVeterinarios = await _context.EspecialidadesVeterinario
                .Where(especialidadVeterinario => !especialidadVeterinario.isDeleted)
                .Select(especialidadVeterinario => new EspecialidadVeterinarioResponseDto
                {
                    idVeterinarioEspecialidad = especialidadVeterinario.idVeterinarioEspecialidad,
                    idVeterinario = especialidadVeterinario.idVeterinario,
                    idEspecialidad = especialidadVeterinario.idEspecialidad
                })
                .ToListAsync();

            return Ok(especialidadesVeterinarios);
        }

        // GET: api/EspecialidadesVeterinarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadVeterinarioResponseDto>> GetEspecialidadVeterinario(int id)
        {
            var especialidadVeterinario = await _context.EspecialidadesVeterinario
                .Where(especialidadVeterinario => !especialidadVeterinario.isDeleted)
                .Where(especialidadVeterinario => especialidadVeterinario.idVeterinarioEspecialidad == id)
                .Select(especialidadVeterinario => new EspecialidadVeterinarioResponseDto
                {
                    idVeterinarioEspecialidad = especialidadVeterinario.idVeterinarioEspecialidad,
                    idVeterinario = especialidadVeterinario.idVeterinario,
                    idEspecialidad = especialidadVeterinario.idEspecialidad
                })
                .FirstOrDefaultAsync();

            if (especialidadVeterinario == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la asignación de especialidad a veterinario con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(especialidadVeterinario);
        }

        // POST: api/EspecialidadesVeterinarios
        [HttpPost]
        public async Task<ActionResult<EspecialidadVeterinarioResponseDto>> PostEspecialidadVeterinario([FromForm] EspecialidadVeterinarioFormDto request)
        {
            var existeVeterinario = await _context.Veterinarios
                .AnyAsync(veterinario =>
                    !veterinario.isDeleted &&
                    veterinario.idPersonaVet == request.idVeterinario);

            if (!existeVeterinario)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un veterinario con el id proporcionado. (ID: " + request.idVeterinario + ")"
                });
            }

            var existeEspecialidad = await _context.Especialidades
                .AnyAsync(especialidad =>
                    !especialidad.isDeleted &&
                    especialidad.idEspecialidad == request.idEspecialidad);

            if (!existeEspecialidad)
            {
                return BadRequest(new
                {
                    mensaje = "No existe una especialidad con el id proporcionado. (ID: " + request.idEspecialidad + ")"
                });
            }

            var existeAsignacion = await _context.EspecialidadesVeterinario
                .AnyAsync(especialidadVeterinario =>
                    !especialidadVeterinario.isDeleted &&
                    especialidadVeterinario.idVeterinario == request.idVeterinario &&
                    especialidadVeterinario.idEspecialidad == request.idEspecialidad);

            if (existeAsignacion)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una asignación activa entre este veterinario y esta especialidad."
                });
            }

            var nuevaEspecialidadVeterinario = new EspecialidadVeterinario
            {
                idVeterinario = request.idVeterinario,
                idEspecialidad = request.idEspecialidad,
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.EspecialidadesVeterinario.Add(nuevaEspecialidadVeterinario);
            await _context.SaveChangesAsync();

            var responseDto = new EspecialidadVeterinarioResponseDto
            {
                idVeterinarioEspecialidad = nuevaEspecialidadVeterinario.idVeterinarioEspecialidad,
                idVeterinario = nuevaEspecialidadVeterinario.idVeterinario,
                idEspecialidad = nuevaEspecialidadVeterinario.idEspecialidad
            };

            return CreatedAtAction(
                nameof(GetEspecialidadVeterinario),
                new { id = nuevaEspecialidadVeterinario.idVeterinarioEspecialidad },
                responseDto
            );
        }

        // PUT: api/EspecialidadesVeterinarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidadVeterinario(int id, [FromForm] EspecialidadVeterinarioFormDto request)
        {
            var especialidadVeterinarioExistente = await _context.EspecialidadesVeterinario
                .FirstOrDefaultAsync(especialidadVeterinario =>
                    !especialidadVeterinario.isDeleted &&
                    especialidadVeterinario.idVeterinarioEspecialidad == id);

            if (especialidadVeterinarioExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la asignación de especialidad a veterinario con el id proporcionado. (ID: " + id + ")"
                });
            }

            var existeVeterinario = await _context.Veterinarios
                .AnyAsync(veterinario =>
                    !veterinario.isDeleted &&
                    veterinario.idPersonaVet == request.idVeterinario);

            if (!existeVeterinario)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un veterinario con el id proporcionado. (ID: " + request.idVeterinario + ")"
                });
            }

            var existeEspecialidad = await _context.Especialidades
                .AnyAsync(especialidad =>
                    !especialidad.isDeleted &&
                    especialidad.idEspecialidad == request.idEspecialidad);

            if (!existeEspecialidad)
            {
                return BadRequest(new
                {
                    mensaje = "No existe una especialidad con el id proporcionado. (ID: " + request.idEspecialidad + ")"
                });
            }

            var existeOtraAsignacion = await _context.EspecialidadesVeterinario
                .Where(especialidadVeterinario => !especialidadVeterinario.isDeleted)
                .AnyAsync(especialidadVeterinario =>
                    especialidadVeterinario.idVeterinarioEspecialidad != id &&
                    especialidadVeterinario.idVeterinario == request.idVeterinario &&
                    especialidadVeterinario.idEspecialidad == request.idEspecialidad);

            if (existeOtraAsignacion)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otra asignación activa entre este veterinario y esta especialidad."
                });
            }

            especialidadVeterinarioExistente.idVeterinario = request.idVeterinario;
            especialidadVeterinarioExistente.idEspecialidad = request.idEspecialidad;
            especialidadVeterinarioExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(especialidadVeterinarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La asignación de especialidad a veterinario ha sido actualizada exitosamente. (ID: " + id + ")"
            });
        }

        // POST: api/EspecialidadesVeterinarios/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteEspecialidadVeterinario(int id)
        {
            var especialidadVeterinarioExistente = await _context.EspecialidadesVeterinario
                .FirstOrDefaultAsync(especialidadVeterinario =>
                    !especialidadVeterinario.isDeleted &&
                    especialidadVeterinario.idVeterinarioEspecialidad == id);

            if (especialidadVeterinarioExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la asignación de especialidad a veterinario con el id proporcionado. (ID: " + id + ")"
                });
            }

            especialidadVeterinarioExistente.isDeleted = true;
            especialidadVeterinarioExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(especialidadVeterinarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La asignación de especialidad a veterinario ha sido eliminada exitosamente. (ID: " + id + ")"
            });
        }
    }
}