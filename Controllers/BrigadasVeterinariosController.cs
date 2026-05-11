using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.BrigadasVeterinarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrigadasVeterinariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public BrigadasVeterinariosController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/BrigadasVeterinarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrigadaVeterinarioResponseDto>>> GetBrigadasVeterinarios()
        {
            var brigadasVeterinarios = await _context.BrigadasVeterinario
                .Where(brigadaVeterinario => !brigadaVeterinario.isDeleted)
                .Select(brigadaVeterinario => new BrigadaVeterinarioResponseDto
                {
                    idBrigadaVeterinario = brigadaVeterinario.idBrigadaVeterinario,
                    idVeterinario = brigadaVeterinario.idVeterinario,
                    idBrigada = brigadaVeterinario.idBrigada,
                    idRolParticipacion = brigadaVeterinario.idRolParticipacion
                })
                .ToListAsync();

            return Ok(brigadasVeterinarios);
        }

        // GET: api/BrigadasVeterinarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrigadaVeterinarioResponseDto>> GetBrigadaVeterinario(int id)
        {
            var brigadaVeterinario = await _context.BrigadasVeterinario
                .Where(brigadaVeterinario => !brigadaVeterinario.isDeleted)
                .Where(brigadaVeterinario => brigadaVeterinario.idBrigadaVeterinario == id)
                .Select(brigadaVeterinario => new BrigadaVeterinarioResponseDto
                {
                    idBrigadaVeterinario = brigadaVeterinario.idBrigadaVeterinario,
                    idVeterinario = brigadaVeterinario.idVeterinario,
                    idBrigada = brigadaVeterinario.idBrigada,
                    idRolParticipacion = brigadaVeterinario.idRolParticipacion
                })
                .FirstOrDefaultAsync();

            if (brigadaVeterinario == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la asignación de veterinario a brigada con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(brigadaVeterinario);
        }

        // POST: api/BrigadasVeterinarios
        [HttpPost]
        public async Task<ActionResult<BrigadaVeterinarioResponseDto>> PostBrigadaVeterinario([FromForm] BrigadaVeterinarioFormDto request)
        {
            var existeVeterinario = await _context.Veterinarios
                .AnyAsync(veterinario => !veterinario.isDeleted && veterinario.idPersonaVet == request.idVeterinario);

            if (!existeVeterinario)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un veterinario con el id proporcionado. (ID: " + request.idVeterinario + ")"
                });
            }

            var existeBrigada = await _context.Brigadas
                .AnyAsync(brigada => !brigada.isDeleted && brigada.idBrigada == request.idBrigada);

            if (!existeBrigada)
            {
                return BadRequest(new
                {
                    mensaje = "No existe una brigada con el id proporcionado. (ID: " + request.idBrigada + ")"
                });
            }

            var existeRolParticipacion = await _context.RolesParticipacion
                .AnyAsync(rol => !rol.isDeleted && rol.idRolParticipacion == request.idRolParticipacion);

            if (!existeRolParticipacion)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un rol de participación con el id proporcionado. (ID: " + request.idRolParticipacion + ")"
                });
            }

            var existeAsignacion = await _context.BrigadasVeterinario
                .AnyAsync(brigadaVeterinario =>
                    !brigadaVeterinario.isDeleted &&
                    brigadaVeterinario.idVeterinario == request.idVeterinario &&
                    brigadaVeterinario.idBrigada == request.idBrigada &&
                    brigadaVeterinario.idRolParticipacion == request.idRolParticipacion);

            if (existeAsignacion)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una asignación activa para este veterinario, brigada y rol de participación."
                });
            }

            var nuevaBrigadaVeterinario = new BrigadaVeterinario
            {
                idVeterinario = request.idVeterinario,
                idBrigada = request.idBrigada,
                idRolParticipacion = request.idRolParticipacion,
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.BrigadasVeterinario.Add(nuevaBrigadaVeterinario);
            await _context.SaveChangesAsync();

            var responseDto = new BrigadaVeterinarioResponseDto
            {
                idBrigadaVeterinario = nuevaBrigadaVeterinario.idBrigadaVeterinario,
                idVeterinario = nuevaBrigadaVeterinario.idVeterinario,
                idBrigada = nuevaBrigadaVeterinario.idBrigada,
                idRolParticipacion = nuevaBrigadaVeterinario.idRolParticipacion
            };

            return CreatedAtAction(
                nameof(GetBrigadaVeterinario),
                new { id = nuevaBrigadaVeterinario.idBrigadaVeterinario },
                responseDto
            );
        }

        // PUT: api/BrigadasVeterinarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrigadaVeterinario(int id, [FromForm] BrigadaVeterinarioFormDto request)
        {
            var brigadaVeterinarioExistente = await _context.BrigadasVeterinario
                .FirstOrDefaultAsync(brigadaVeterinario =>
                    !brigadaVeterinario.isDeleted &&
                    brigadaVeterinario.idBrigadaVeterinario == id);

            if (brigadaVeterinarioExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la asignación de veterinario a brigada con el id proporcionado. (ID: " + id + ")"
                });
            }

            var existeVeterinario = await _context.Veterinarios
                .AnyAsync(veterinario => !veterinario.isDeleted && veterinario.idPersonaVet == request.idVeterinario);

            if (!existeVeterinario)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un veterinario con el id proporcionado. (ID: " + request.idVeterinario + ")"
                });
            }

            var existeBrigada = await _context.Brigadas
                .AnyAsync(brigada => !brigada.isDeleted && brigada.idBrigada == request.idBrigada);

            if (!existeBrigada)
            {
                return BadRequest(new
                {
                    mensaje = "No existe una brigada con el id proporcionado. (ID: " + request.idBrigada + ")"
                });
            }

            var existeRolParticipacion = await _context.RolesParticipacion
                .AnyAsync(rol => !rol.isDeleted && rol.idRolParticipacion == request.idRolParticipacion);

            if (!existeRolParticipacion)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un rol de participación con el id proporcionado. (ID: " + request.idRolParticipacion + ")"
                });
            }

            var existeOtraAsignacion = await _context.BrigadasVeterinario
                .Where(brigadaVeterinario => !brigadaVeterinario.isDeleted)
                .AnyAsync(brigadaVeterinario =>
                    brigadaVeterinario.idBrigadaVeterinario != id &&
                    brigadaVeterinario.idVeterinario == request.idVeterinario &&
                    brigadaVeterinario.idBrigada == request.idBrigada &&
                    brigadaVeterinario.idRolParticipacion == request.idRolParticipacion);

            if (existeOtraAsignacion)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otra asignación activa para este veterinario, brigada y rol de participación."
                });
            }

            brigadaVeterinarioExistente.idVeterinario = request.idVeterinario;
            brigadaVeterinarioExistente.idBrigada = request.idBrigada;
            brigadaVeterinarioExistente.idRolParticipacion = request.idRolParticipacion;
            brigadaVeterinarioExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(brigadaVeterinarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La asignación del veterinario a la brigada ha sido actualizada exitosamente. (ID: " + id + ")"
            });
        }

        // POST: api/BrigadasVeterinarios/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteBrigadaVeterinario(int id)
        {
            var brigadaVeterinarioExistente = await _context.BrigadasVeterinario
                .FirstOrDefaultAsync(brigadaVeterinario =>
                    !brigadaVeterinario.isDeleted &&
                    brigadaVeterinario.idBrigadaVeterinario == id);

            if (brigadaVeterinarioExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la asignación de veterinario a brigada con el id proporcionado. (ID: " + id + ")"
                });
            }

            brigadaVeterinarioExistente.isDeleted = true;
            brigadaVeterinarioExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(brigadaVeterinarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La asignación del veterinario a la brigada ha sido eliminada exitosamente. (ID: " + id + ")"
            });
        }
    }
}