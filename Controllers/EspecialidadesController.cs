using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Especialidades;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspecialidadesController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadResponseDto>>> GetEspecialidades()
        {
            var especialidades = await _context.Especialidades
                .Where(especialidad => !especialidad.isDeleted)
                .Select(especialidad => new EspecialidadResponseDto
                {
                    idEspecialidad = especialidad.idEspecialidad,
                    especialidad = especialidad.especialidad,
                    descripcion = especialidad.descripcion
                })
                .ToListAsync();

            return Ok(especialidades);
        }

        // GET: api/Especialidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadResponseDto>> GetEspecialidad(int id)
        {
            var especialidad = await _context.Especialidades
                .Where(especialidad => !especialidad.isDeleted)
                .Where(especialidad => especialidad.idEspecialidad == id)
                .Select(especialidad => new EspecialidadResponseDto
                {
                    idEspecialidad = especialidad.idEspecialidad,
                    especialidad = especialidad.especialidad,
                    descripcion = especialidad.descripcion
                })
                .FirstOrDefaultAsync();

            if (especialidad == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la especialidad con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(especialidad);
        }

        // POST: api/Especialidades
        [HttpPost]
        public async Task<ActionResult<EspecialidadResponseDto>> PostEspecialidad([FromForm] EspecialidadFormDto request)
        {
            if (string.IsNullOrWhiteSpace(request.especialidad))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'especialidad' es obligatorio y no puede estar vacío."
                });
            }

            var existeEspecialidad = await _context.Especialidades
                .AnyAsync(especialidad =>
                    !especialidad.isDeleted &&
                    especialidad.especialidad.ToLower() == request.especialidad.ToLower());

            if (existeEspecialidad)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una especialidad con el mismo nombre. (Nombre ingresado: " + request.especialidad + ")"
                });
            }

            var nuevaEspecialidad = new Especialidad
            {
                especialidad = request.especialidad.Trim(),
                descripcion = request.descripcion?.Trim(),
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.Especialidades.Add(nuevaEspecialidad);
            await _context.SaveChangesAsync();

            var responseDto = new EspecialidadResponseDto
            {
                idEspecialidad = nuevaEspecialidad.idEspecialidad,
                especialidad = nuevaEspecialidad.especialidad,
                descripcion = nuevaEspecialidad.descripcion
            };

            return CreatedAtAction(
                nameof(GetEspecialidad),
                new { id = nuevaEspecialidad.idEspecialidad },
                responseDto
            );
        }

        // PUT: api/Especialidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidad(int id, [FromForm] EspecialidadFormDto request)
        {
            var especialidadExistente = await _context.Especialidades
                .FirstOrDefaultAsync(especialidad =>
                    !especialidad.isDeleted &&
                    especialidad.idEspecialidad == id);

            if (especialidadExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la especialidad con el id proporcionado. (ID: " + id + ")"
                });
            }

            if (string.IsNullOrWhiteSpace(request.especialidad))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'especialidad' es obligatorio y no puede estar vacío."
                });
            }

            var existeOtraEspecialidadConMismoNombre = await _context.Especialidades
                .Where(especialidad => !especialidad.isDeleted)
                .AnyAsync(especialidad =>
                    especialidad.idEspecialidad != id &&
                    especialidad.especialidad.ToLower() == request.especialidad.ToLower());

            if (existeOtraEspecialidadConMismoNombre)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otra especialidad con el mismo nombre. (Nombre ingresado: " + request.especialidad + ")"
                });
            }

            especialidadExistente.especialidad = request.especialidad.Trim();
            especialidadExistente.descripcion = string.IsNullOrWhiteSpace(request.descripcion)
                ? especialidadExistente.descripcion
                : request.descripcion.Trim();
            especialidadExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(especialidadExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La especialidad " + especialidadExistente.especialidad + " ha sido actualizada exitosamente. (ID: " + id + " - " + especialidadExistente.especialidad + ")"
            });
        }

        // POST: api/Especialidades/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var especialidadExistente = await _context.Especialidades
                .FirstOrDefaultAsync(especialidad =>
                    !especialidad.isDeleted &&
                    especialidad.idEspecialidad == id);

            if (especialidadExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la especialidad con el id proporcionado. (ID: " + id + ")"
                });
            }

            especialidadExistente.isDeleted = true;
            especialidadExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(especialidadExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La especialidad ha sido eliminada exitosamente. (ID: " + id + ")"
            });
        }
    }
}