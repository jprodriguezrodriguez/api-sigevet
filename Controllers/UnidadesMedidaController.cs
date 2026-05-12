using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.UnidadesMedida;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public UnidadesMedidaController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadesMedidaResponseDto>>> GetUnidadesMedida()
        {
            var unidades = await _context.UnidadesMedida
                .Where(u => !u.isDeleted)
                .Select(u => new UnidadesMedidaResponseDto
                {
                    idUnidadMedida = u.idUnidadMedida,
                    unidadMedida = u.unidadMedida,
                    descripcion = u.descripcion
                }).ToListAsync();

            return Ok(unidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadesMedidaResponseDto>> GetUnidadMedida(int id)
        {
            var unidad = await _context.UnidadesMedida
                .Where(u => !u.isDeleted && u.idUnidadMedida == id)
                .Select(u => new UnidadesMedidaResponseDto
                {
                    idUnidadMedida = u.idUnidadMedida,
                    unidadMedida = u.unidadMedida,
                    descripcion = u.descripcion
                }).FirstOrDefaultAsync();

            if (unidad == null)
            {
                return NotFound();
            }

            return Ok(unidad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadMedida(int id, [FromForm] UnidadesMedidaFormDto unidadDto)
        {
            var unidad = await _context.UnidadesMedida.FindAsync(id);
            if (unidad == null || unidad.isDeleted)
            {
                return NotFound();
            }

            unidad.unidadMedida = unidadDto.unidadMedida;
            unidad.descripcion = unidadDto.descripcion;

            _context.UnidadesMedida.Update(unidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UnidadesMedidaResponseDto>> PostUnidadMedida([FromForm] UnidadesMedidaFormDto unidadDto)
        {
            var unidad = new UnidadMedida
            {
                unidadMedida = unidadDto.unidadMedida,
                descripcion = unidadDto.descripcion
            };

            _context.UnidadesMedida.Add(unidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUnidadMedida), new { id = unidad.idUnidadMedida }, new UnidadesMedidaResponseDto
            {
                idUnidadMedida = unidad.idUnidadMedida,
                unidadMedida = unidad.unidadMedida,
                descripcion = unidad.descripcion
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidadMedida(int id)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida == null || unidadMedida.isDeleted)
            {
                return NotFound();
            }

            unidadMedida.isDeleted = true;
            _context.UnidadesMedida.Update(unidadMedida);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
