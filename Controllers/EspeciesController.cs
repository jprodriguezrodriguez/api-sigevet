using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Especies;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspeciesController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecieResponseDto>>> GetEspecies()
        {
            var especies = await _context.Especies
                .Where(especie => !especie.isDeleted)
                .Select(especie => new EspecieResponseDto
                {
                    idEspecie = especie.idEspecie,
                    especie = especie.especie,
                    descripcion = especie.descripcion
                })
                .ToListAsync();

            return Ok(especies);
        }

        // GET: api/Especies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecieResponseDto>> GetEspecie(int id)
        {
            var especie = await _context.Especies
                .Where(especie => !especie.isDeleted)
                .Where(especie => especie.idEspecie == id)
                .Select(especie => new EspecieResponseDto
                {
                    idEspecie = especie.idEspecie,
                    especie = especie.especie,
                    descripcion = especie.descripcion
                })
                .FirstOrDefaultAsync();

            if (especie == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la especie con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(especie);
        }

        // POST: api/Especies
        [HttpPost]
        public async Task<ActionResult<EspecieResponseDto>> PostEspecie([FromForm] EspecieFormDto request)
        {
            if (string.IsNullOrWhiteSpace(request.especie))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'especie' es obligatorio y no puede estar vacío."
                });
            }

            var existeEspecie = await _context.Especies
                .AnyAsync(especie =>
                    !especie.isDeleted &&
                    especie.especie.ToLower() == request.especie.ToLower());

            if (existeEspecie)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una especie con el mismo nombre. (Nombre ingresado: " + request.especie + ")"
                });
            }

            var nuevaEspecie = new Especie
            {
                especie = request.especie.Trim(),
                descripcion = request.descripcion?.Trim(),
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.Especies.Add(nuevaEspecie);
            await _context.SaveChangesAsync();

            var responseDto = new EspecieResponseDto
            {
                idEspecie = nuevaEspecie.idEspecie,
                especie = nuevaEspecie.especie,
                descripcion = nuevaEspecie.descripcion
            };

            return CreatedAtAction(
                nameof(GetEspecie),
                new { id = nuevaEspecie.idEspecie },
                responseDto
            );
        }

        // PUT: api/Especies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecie(int id, [FromForm] EspecieFormDto request)
        {
            var especieExistente = await _context.Especies
                .FirstOrDefaultAsync(especie =>
                    !especie.isDeleted &&
                    especie.idEspecie == id);

            if (especieExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la especie con el id proporcionado. (ID: " + id + ")"
                });
            }

            if (string.IsNullOrWhiteSpace(request.especie))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'especie' es obligatorio y no puede estar vacío."
                });
            }

            var existeOtraEspecieConMismoNombre = await _context.Especies
                .Where(especie => !especie.isDeleted)
                .AnyAsync(especie =>
                    especie.idEspecie != id &&
                    especie.especie.ToLower() == request.especie.ToLower());

            if (existeOtraEspecieConMismoNombre)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otra especie con el mismo nombre. (Nombre ingresado: " + request.especie + ")"
                });
            }

            especieExistente.especie = request.especie.Trim();
            especieExistente.descripcion = string.IsNullOrWhiteSpace(request.descripcion)
                ? especieExistente.descripcion
                : request.descripcion.Trim();
            especieExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(especieExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La especie " + especieExistente.especie + " ha sido actualizada exitosamente. (ID: " + id + " - " + especieExistente.especie + ")"
            });
        }

        // POST: api/Especies/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteEspecie(int id)
        {
            var especieExistente = await _context.Especies
                .FirstOrDefaultAsync(especie =>
                    !especie.isDeleted &&
                    especie.idEspecie == id);

            if (especieExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la especie con el id proporcionado. (ID: " + id + ")"
                });
            }

            especieExistente.isDeleted = true;
            especieExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(especieExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La especie ha sido eliminada exitosamente. (ID: " + id + ")"
            });
        }
    }
}