using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Estados;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EstadosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosResponseDto>>> GetEstados()
        {
            var estados = await _context.Estados
                .Include(e => e.categoriaEstado)
                .Where(e => !e.isDeleted)
                .Select(e => new EstadosResponseDto
                {
                    idEstado = e.idEstado,
                    estado = e.estado,
                    descripcion = e.descripcion,
                    categoriaEstado = e.categoriaEstado != null ? e.categoriaEstado.categoriaEstado : null
                }).ToListAsync();

            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadosResponseDto>> GetEstado(int id)
        {
            var estado = await _context.Estados
                .Include(e => e.categoriaEstado)
                .Where(e => !e.isDeleted && e.idEstado == id)
                .Select(e => new EstadosResponseDto
                {
                    idEstado = e.idEstado,
                    estado = e.estado,
                    descripcion = e.descripcion,
                    categoriaEstado = e.categoriaEstado != null ? e.categoriaEstado.categoriaEstado : null
                }).FirstOrDefaultAsync();

            if (estado == null)
            {
                return NotFound();
            }

            return Ok(estado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, [FromForm] EstadosFormDto estadoDto)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null || estado.isDeleted)
            {
                return NotFound();
            }

            estado.estado = estadoDto.estado;
            estado.descripcion = estadoDto.descripcion;
            estado.idCategoriaEstado = estadoDto.idCategoriaEstado;

            _context.Estados.Update(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EstadosResponseDto>> PostEstado([FromForm] EstadosFormDto estadoDto)
        {
            var estado = new Estado
            {
                estado = estadoDto.estado,
                descripcion = estadoDto.descripcion,
                idCategoriaEstado = estadoDto.idCategoriaEstado
            };

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstado), new { id = estado.idEstado }, estado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null || estado.isDeleted)
            {
                return NotFound();
            }

            estado.isDeleted = true;
            _context.Estados.Update(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("por-categoria/{idCategoriaEstado}")]
        public async Task<ActionResult<IEnumerable<EstadosResponseDto>>> GetEstadosPorCategoria(int idCategoriaEstado)
        {
            var categoriaExiste = await _context.CategoriasEstado.AnyAsync(c => c.idCategoriaEstado == idCategoriaEstado && !c.isDeleted);
            if (!categoriaExiste)
            {
                return NotFound($"No existe una categoria de estado con ID {idCategoriaEstado}.");
            }

            var estados = await _context.Estados
                .Include(e => e.categoriaEstado)
                .Where(e => e.idCategoriaEstado == idCategoriaEstado && !e.isDeleted)
                .Select(e => new EstadosResponseDto
                {
                    idEstado = e.idEstado,
                    estado = e.estado,
                    descripcion = e.descripcion,
                    categoriaEstado = e.categoriaEstado != null ? e.categoriaEstado.categoriaEstado : null
                }).ToListAsync();

            return Ok(estados);
        }
    }
}
