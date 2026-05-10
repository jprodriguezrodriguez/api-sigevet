using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sigevet.DTOs.TiposIdentificacion;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposIdentificacionController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposIdentificacionController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoIdentificacionResponseDto>>> GetTiposIdentificacion()
        {
            var tiposIdentificacion = await _context.TiposIdentificacion
                .Where(tipId => !tipId.isDeleted)
                .Select(tipId => new TipoIdentificacionResponseDto
                {
                    idTipoIdentificacion = tipId.idTipoIdentificacion,
                    tipoIdentificacion = tipId.tipoIdentificacion,
                    descripcion = tipId.descripcion
                }).ToListAsync();
            return Ok(tiposIdentificacion);
        }

        // GET: api/TipoIdentificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIdentificacionResponseDto>> GetTipoIdentificacion(int id)
        {
            var tiposIdentificacion = await _context.TiposIdentificacion
                .Where(tipId => !tipId.isDeleted)
                .Where(tipId => tipId.idTipoIdentificacion == id)
                .Select(tipId => new TipoIdentificacionResponseDto
                {
                    idTipoIdentificacion = tipId.idTipoIdentificacion,
                    tipoIdentificacion = tipId.tipoIdentificacion,
                    descripcion = tipId.descripcion
                }).FirstOrDefaultAsync();

            if (tiposIdentificacion == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el tipo de identificación con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(tiposIdentificacion);
        }

        // POST: api/TipoIdentificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoIdentificacionResponseDto>> PostTipoIdentificacion([FromForm] TipoIdentificacionFormDto request)
        {
            if (string.IsNullOrWhiteSpace(request.tipoIdentificacion))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'tipoIdentificacion' es obligatorio y no puede estar vacío."
                });
            }

            var existeTipoIdentificacion = await _context.TiposIdentificacion
                .AnyAsync(tipId => !tipId.isDeleted &&
                tipId.tipoIdentificacion.ToLower() == request.tipoIdentificacion.ToLower());

            if (existeTipoIdentificacion) {
                return BadRequest(new
                {
                    mensaje = "Ya existe un tipo de identificación con el mismo nombre. (Nombre ingresado: " + request.tipoIdentificacion + ")"
                });
            }

            var nuevoTipoIdentificacion = new TipoIdentificacion
            {
                tipoIdentificacion = request.tipoIdentificacion.Trim(),
                descripcion = request.descripcion?.Trim(),
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.TiposIdentificacion.Add(nuevoTipoIdentificacion);
            await _context.SaveChangesAsync();

            var responseDto = new TipoIdentificacionResponseDto
            {
                idTipoIdentificacion = nuevoTipoIdentificacion.idTipoIdentificacion,
                tipoIdentificacion = nuevoTipoIdentificacion.tipoIdentificacion,
                descripcion = nuevoTipoIdentificacion.descripcion
            };

            return CreatedAtAction(
                nameof(GetTipoIdentificacion),
                new { id = nuevoTipoIdentificacion.idTipoIdentificacion }, responseDto);

        }

        // PUT: api/TipoIdentificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoIdentificacion(int id, [FromForm] TipoIdentificacionFormDto request)
        {

            var tipoIdentificacionExistente = await _context.TiposIdentificacion
                .FirstOrDefaultAsync(tipId => !tipId.isDeleted && tipId.idTipoIdentificacion == id);

            if (tipoIdentificacionExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el tipo de identificación con el id proporcionado. (ID: " + id + ")"
                });
            }

            var existeOtroTipoIdentificacionConMismoNombre = await _context.TiposIdentificacion
                .Where(tipId => !tipId.isDeleted)
                .AnyAsync(tipId => tipId.idTipoIdentificacion != id &&
                tipId.tipoIdentificacion.ToLower() == request.tipoIdentificacion.ToLower());

            if (existeOtroTipoIdentificacionConMismoNombre)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otro tipo de identificación con el mismo nombre. (Nombre ingresado: " + request.tipoIdentificacion + ")"
                });
            }


            tipoIdentificacionExistente.tipoIdentificacion = request.tipoIdentificacion.Trim();
            tipoIdentificacionExistente.descripcion = request.descripcion.IsNullOrEmpty() ? tipoIdentificacionExistente.descripcion : request.descripcion?.Trim();
            tipoIdentificacionExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(tipoIdentificacionExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/TiposIdentificacion/delete
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteTipoIdentificacion(int id)
        {
            var tipoIdentificacionExistente = await _context.TiposIdentificacion
                .FirstOrDefaultAsync(tipId => !tipId.isDeleted && tipId.idTipoIdentificacion == id);

            if (tipoIdentificacionExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el tipo de identificación con el id proporcionado. (ID: " + id + ")"
                });
            }

            tipoIdentificacionExistente.isDeleted = true;
            tipoIdentificacionExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(tipoIdentificacionExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "El tipo de identificación ha sido eliminado exitosamente. (ID: " + id + ")"
            });
        }
    }
}
