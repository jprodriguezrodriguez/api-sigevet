using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Brigadas;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrigadasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public BrigadasController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrigadaResponseDto>>> GetBrigadas()
        {
            var brigadas = await _context.Brigadas
                .Where(brigada => !brigada.isDeleted)
                .Select(brigada => new BrigadaResponseDto
                {
                    idBrigada = brigada.idBrigada,
                    nombreBrigada = brigada.nombreBrigada,
                    fechaBrigada = brigada.fechaBrigada,
                    horaInicio = brigada.horaInicio,
                    horaFin = brigada.horaFin,
                    ubicacion = brigada.ubicacion,
                    cobertura = brigada.cobertura,
                    observaciones = brigada.observaciones,
                    idEstadoBrigada = brigada.idEstadoBrigada
                })
                .ToListAsync();

            return Ok(brigadas);
        }

        // GET: api/Brigadas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrigadaResponseDto>> GetBrigada(int id)
        {
            var brigada = await _context.Brigadas
                .Where(brigada => !brigada.isDeleted)
                .Where(brigada => brigada.idBrigada == id)
                .Select(brigada => new BrigadaResponseDto
                {
                    idBrigada = brigada.idBrigada,
                    nombreBrigada = brigada.nombreBrigada,
                    fechaBrigada = brigada.fechaBrigada,
                    horaInicio = brigada.horaInicio,
                    horaFin = brigada.horaFin,
                    ubicacion = brigada.ubicacion,
                    cobertura = brigada.cobertura,
                    observaciones = brigada.observaciones,
                    idEstadoBrigada = brigada.idEstadoBrigada
                })
                .FirstOrDefaultAsync();

            if (brigada == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la brigada con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(brigada);
        }

        // POST: api/Brigadas
        [HttpPost]
        public async Task<ActionResult<BrigadaResponseDto>> PostBrigada([FromForm] BrigadaFormDto request)
        {
            if (string.IsNullOrWhiteSpace(request.nombreBrigada))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'nombreBrigada' es obligatorio y no puede estar vacío."
                });
            }

            if (string.IsNullOrWhiteSpace(request.ubicacion))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'ubicacion' es obligatorio y no puede estar vacío."
                });
            }

            if (request.horaFin <= request.horaInicio)
            {
                return BadRequest(new
                {
                    mensaje = "La hora de finalización debe ser mayor que la hora de inicio."
                });
            }

            var existeEstadoBrigada = await _context.Estados
                .AnyAsync(estado => !estado.isDeleted && estado.idEstado == request.idEstadoBrigada);

            if (!existeEstadoBrigada)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un estado de brigada con el id proporcionado. (ID: " + request.idEstadoBrigada + ")"
                });
            }

            var existeBrigada = await _context.Brigadas
                .AnyAsync(brigada => !brigada.isDeleted &&
                    brigada.nombreBrigada.ToLower() == request.nombreBrigada.ToLower() &&
                    brigada.fechaBrigada == request.fechaBrigada);

            if (existeBrigada)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una brigada con el mismo nombre y fecha. (Nombre ingresado: " + request.nombreBrigada + ")"
                });
            }

            var nuevaBrigada = new Brigada
            {
                nombreBrigada = request.nombreBrigada.Trim(),
                fechaBrigada = request.fechaBrigada,
                horaInicio = request.horaInicio,
                horaFin = request.horaFin,
                ubicacion = request.ubicacion.Trim(),
                cobertura = request.cobertura?.Trim(),
                observaciones = request.observaciones?.Trim(),
                idEstadoBrigada = request.idEstadoBrigada,
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.Brigadas.Add(nuevaBrigada);
            await _context.SaveChangesAsync();

            var responseDto = new BrigadaResponseDto
            {
                idBrigada = nuevaBrigada.idBrigada,
                nombreBrigada = nuevaBrigada.nombreBrigada,
                fechaBrigada = nuevaBrigada.fechaBrigada,
                horaInicio = nuevaBrigada.horaInicio,
                horaFin = nuevaBrigada.horaFin,
                ubicacion = nuevaBrigada.ubicacion,
                cobertura = nuevaBrigada.cobertura,
                observaciones = nuevaBrigada.observaciones,
                idEstadoBrigada = nuevaBrigada.idEstadoBrigada
            };

            return CreatedAtAction(
                nameof(GetBrigada),
                new { id = nuevaBrigada.idBrigada },
                responseDto
            );
        }

        // PUT: api/Brigadas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrigada(int id, [FromForm] BrigadaFormDto request)
        {
            var brigadaExistente = await _context.Brigadas
                .FirstOrDefaultAsync(brigada => !brigada.isDeleted && brigada.idBrigada == id);

            if (brigadaExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la brigada con el id proporcionado. (ID: " + id + ")"
                });
            }

            if (string.IsNullOrWhiteSpace(request.nombreBrigada))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'nombreBrigada' es obligatorio y no puede estar vacío."
                });
            }

            if (string.IsNullOrWhiteSpace(request.ubicacion))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'ubicacion' es obligatorio y no puede estar vacío."
                });
            }

            if (request.horaFin <= request.horaInicio)
            {
                return BadRequest(new
                {
                    mensaje = "La hora de finalización debe ser mayor que la hora de inicio."
                });
            }

            var existeEstadoBrigada = await _context.Estados
                .AnyAsync(estado => !estado.isDeleted && estado.idEstado == request.idEstadoBrigada);

            if (!existeEstadoBrigada)
            {
                return BadRequest(new
                {
                    mensaje = "No existe un estado de brigada con el id proporcionado. (ID: " + request.idEstadoBrigada + ")"
                });
            }

            var existeOtraBrigadaConMismoNombreYFecha = await _context.Brigadas
                .Where(brigada => !brigada.isDeleted)
                .AnyAsync(brigada =>
                    brigada.idBrigada != id &&
                    brigada.nombreBrigada.ToLower() == request.nombreBrigada.ToLower() &&
                    brigada.fechaBrigada == request.fechaBrigada);

            if (existeOtraBrigadaConMismoNombreYFecha)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otra brigada con el mismo nombre y fecha. (Nombre ingresado: " + request.nombreBrigada + ")"
                });
            }

            brigadaExistente.nombreBrigada = request.nombreBrigada.Trim();
            brigadaExistente.fechaBrigada = request.fechaBrigada;
            brigadaExistente.horaInicio = request.horaInicio;
            brigadaExistente.horaFin = request.horaFin;
            brigadaExistente.ubicacion = request.ubicacion.Trim();
            brigadaExistente.cobertura = string.IsNullOrWhiteSpace(request.cobertura)
                ? brigadaExistente.cobertura
                : request.cobertura.Trim();
            brigadaExistente.observaciones = string.IsNullOrWhiteSpace(request.observaciones)
                ? brigadaExistente.observaciones
                : request.observaciones.Trim();
            brigadaExistente.idEstadoBrigada = request.idEstadoBrigada;
            brigadaExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(brigadaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La brigada " + brigadaExistente.nombreBrigada + " ha sido actualizada exitosamente. (ID: " + id + " - " + brigadaExistente.nombreBrigada + ")"
            });
        }

        // POST: api/Brigadas/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteBrigada(int id)
        {
            var brigadaExistente = await _context.Brigadas
                .FirstOrDefaultAsync(brigada => !brigada.isDeleted && brigada.idBrigada == id);

            if (brigadaExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la brigada con el id proporcionado. (ID: " + id + ")"
                });
            }

            brigadaExistente.isDeleted = true;
            brigadaExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(brigadaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La brigada ha sido eliminada exitosamente. (ID: " + id + ")"
            });
        }
    }
}