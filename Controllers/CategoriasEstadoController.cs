using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.CategoriasEstado;
using sigevet.DTOs.Estados;
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
        public async Task<ActionResult<IEnumerable<CategoriasEstadoResponseDto>>> GetCategoriasEstado()
        {
            var categorias = await _context.CategoriasEstado
                .Where(c => !c.isDeleted)
                .Select(c => new CategoriasEstadoResponseDto
                {
                    idCategoriaEstado = c.idCategoriaEstado,
                    categoriaEstado = c.categoriaEstado,
                    descripcion = c.descripcion
                }).ToListAsync();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriasEstadoResponseDto>> GetCategoriaEstado(int id)
        {
            var categoria = await _context.CategoriasEstado
                .Where(c => !c.isDeleted && c.idCategoriaEstado == id)
                .Select(c => new CategoriasEstadoResponseDto
                {
                    idCategoriaEstado = c.idCategoriaEstado,
                    categoriaEstado = c.categoriaEstado,
                    descripcion = c.descripcion
                }).FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEstado(int id, CategoriasEstadoFormDto categoriaDto)
        {
            var categoria = await _context.CategoriasEstado.FindAsync(id);
            if (categoria == null || categoria.isDeleted)
            {
                return NotFound();
            }

            categoria.categoriaEstado = categoriaDto.categoriaEstado;
            categoria.descripcion = categoriaDto.descripcion;

            _context.CategoriasEstado.Update(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CategoriasEstadoResponseDto>> PostCategoriaEstado(CategoriasEstadoFormDto categoriaDto)
        {
            var categoria = new CategoriaEstado
            {
                categoriaEstado = categoriaDto.categoriaEstado,
                descripcion = categoriaDto.descripcion
            };

            _context.CategoriasEstado.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoriaEstado), new { id = categoria.idCategoriaEstado }, new CategoriasEstadoResponseDto
            {
                idCategoriaEstado = categoria.idCategoriaEstado,
                categoriaEstado = categoria.categoriaEstado,
                descripcion = categoria.descripcion
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaEstado(int id)
        {
            var categoriaEstado = await _context.CategoriasEstado.FindAsync(id);
            if (categoriaEstado == null || categoriaEstado.isDeleted)
            {
                return NotFound();
            }

            categoriaEstado.isDeleted = true;
            _context.CategoriasEstado.Update(categoriaEstado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("estados-por-categoria/{id}")]
        public async Task<ActionResult<IEnumerable<EstadosResponseDto>>> GetEstadosDeCategoria(int id)
        {
            var categoriaExiste = await _context.CategoriasEstado.AnyAsync(c => c.idCategoriaEstado == id && !c.isDeleted);
            if (!categoriaExiste)
            {
                return NotFound($"No existe una categoria de estado con ID {id}.");
            }

            var estados = await _context.Estados
                .Include(e => e.categoriaEstado)
                .Where(e => e.idCategoriaEstado == id && !e.isDeleted)
                .Select(e => new EstadosResponseDto
                {
                    idEstado = e.idEstado,
                    estado = e.estado,
                    descripcion = e.descripcion,
                    categoriaEstado = e.categoriaEstado != null ? e.categoriaEstado.categoriaEstado : null
                }).ToListAsync();

            return Ok(estados);
        }

        private bool CategoriaEstadoExists(int id)
        {
            return _context.CategoriasEstado.Any(e => e.idCategoriaEstado == id);
        }
    }
}
