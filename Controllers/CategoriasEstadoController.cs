using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.CategoriasEstado;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasEstadoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public CategoriasEstadoController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaEstadoResponseDto>>> GetCategoriasEstado()
        {
            var categoriasEstado = await _context.CategoriasEstado
                .Where(catEstado => !catEstado.isDeleted)
                .Select(catEstado => new CategoriaEstadoResponseDto
                {
                    idCategoriaEstado = catEstado.idCategoriaEstado,
                    categoriaEstado = catEstado.categoriaEstado,
                    descripcion = catEstado.descripcion
                })
                .ToListAsync();

            return Ok(categoriasEstado);
        }

        // GET: api/CategoriasEstado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEstadoResponseDto>> GetCategoriaEstado(int id)
        {
            var categoriaEstado = await _context.CategoriasEstado
                .Where(catEstado => !catEstado.isDeleted)
                .Where(catEstado => catEstado.idCategoriaEstado == id)
                .Select(catEstado => new CategoriaEstadoResponseDto
                {
                    idCategoriaEstado = catEstado.idCategoriaEstado,
                    categoriaEstado = catEstado.categoriaEstado,
                    descripcion = catEstado.descripcion
                })
                .FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la categoría de estado con el id proporcionado. (ID: " + id + ")"
                });
            }

            return Ok(categoriaEstado);
        }

        // POST: api/CategoriasEstado
        [HttpPost]
        public async Task<ActionResult<CategoriaEstadoResponseDto>> PostCategoriaEstado([FromForm] CategoriaEstadoFormDto request)
        {
            if (string.IsNullOrWhiteSpace(request.categoriaEstado))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'categoriaEstado' es obligatorio y no puede estar vacío."
                });
            }

            var existeCategoriaEstado = await _context.CategoriasEstado
                .AnyAsync(catEstado =>
                    !catEstado.isDeleted &&
                    catEstado.categoriaEstado.ToLower() == request.categoriaEstado.ToLower());

            if (existeCategoriaEstado)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe una categoría de estado con el mismo nombre. (Nombre ingresado: " + request.categoriaEstado + ")"
                });
            }

            var nuevaCategoriaEstado = new CategoriaEstado
            {
                categoriaEstado = request.categoriaEstado.Trim(),
                descripcion = request.descripcion?.Trim(),
                isDeleted = false,
                fechaCreacion = DateTime.Now
            };

            _context.CategoriasEstado.Add(nuevaCategoriaEstado);
            await _context.SaveChangesAsync();

            var responseDto = new CategoriaEstadoResponseDto
            {
                idCategoriaEstado = nuevaCategoriaEstado.idCategoriaEstado,
                categoriaEstado = nuevaCategoriaEstado.categoriaEstado,
                descripcion = nuevaCategoriaEstado.descripcion
            };

            return CreatedAtAction(
                nameof(GetCategoriaEstado),
                new { id = nuevaCategoriaEstado.idCategoriaEstado },
                responseDto
            );
        }

        // PUT: api/CategoriasEstado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEstado(int id, [FromForm] CategoriaEstadoFormDto request)
        {
            var categoriaEstadoExistente = await _context.CategoriasEstado
                .FirstOrDefaultAsync(catEstado =>
                    !catEstado.isDeleted &&
                    catEstado.idCategoriaEstado == id);

            if (categoriaEstadoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la categoría de estado con el id proporcionado. (ID: " + id + ")"
                });
            }

            if (string.IsNullOrWhiteSpace(request.categoriaEstado))
            {
                return BadRequest(new
                {
                    mensaje = "El campo 'categoriaEstado' es obligatorio y no puede estar vacío."
                });
            }

            var existeOtraCategoriaEstadoConMismoNombre = await _context.CategoriasEstado
                .Where(catEstado => !catEstado.isDeleted)
                .AnyAsync(catEstado =>
                    catEstado.idCategoriaEstado != id &&
                    catEstado.categoriaEstado.ToLower() == request.categoriaEstado.ToLower());

            if (existeOtraCategoriaEstadoConMismoNombre)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe otra categoría de estado con el mismo nombre. (Nombre ingresado: " + request.categoriaEstado + ")"
                });
            }

            categoriaEstadoExistente.categoriaEstado = request.categoriaEstado.Trim();
            categoriaEstadoExistente.descripcion = string.IsNullOrWhiteSpace(request.descripcion)
                ? categoriaEstadoExistente.descripcion
                : request.descripcion.Trim();
            categoriaEstadoExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(categoriaEstadoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La categoría de estado " + categoriaEstadoExistente.categoriaEstado + " ha sido actualizada exitosamente. (ID: " + id + " - " + categoriaEstadoExistente.categoriaEstado + ")"
            });
        }

        // POST: api/CategoriasEstado/delete/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteCategoriaEstado(int id)
        {
            var categoriaEstadoExistente = await _context.CategoriasEstado
                .FirstOrDefaultAsync(catEstado =>
                    !catEstado.isDeleted &&
                    catEstado.idCategoriaEstado == id);

            if (categoriaEstadoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la categoría de estado con el id proporcionado. (ID: " + id + ")"
                });
            }

            categoriaEstadoExistente.isDeleted = true;
            categoriaEstadoExistente.fechaActualizacion = DateTime.Now;

            _context.Entry(categoriaEstadoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "La categoría de estado ha sido eliminada exitosamente. (ID: " + id + ")"
            });
        }
    }
}