using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.TiposMovimiento;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposMovimientoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposMovimientoController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposMovimientoResponseDto>>> GetTiposMovimiento()
        {
            var tipos = await _context.TiposMovimiento
                .Where(t => !t.isDeleted)
                .Select(t => new TiposMovimientoResponseDto
                {
                    idTipoMovimiento = t.idTipoMovimiento,
                    tipoMovimiento = t.tipoMovimiento,
                    descripcion = t.descripcion
                }).ToListAsync();

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiposMovimientoResponseDto>> GetTipoMovimiento(int id)
        {
            var tipo = await _context.TiposMovimiento
                .Where(t => !t.isDeleted && t.idTipoMovimiento == id)
                .Select(t => new TiposMovimientoResponseDto
                {
                    idTipoMovimiento = t.idTipoMovimiento,
                    tipoMovimiento = t.tipoMovimiento,
                    descripcion = t.descripcion
                }).FirstOrDefaultAsync();

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMovimiento(int id, TiposMovimientoFormDto tipoDto)
        {
            var tipo = await _context.TiposMovimiento.FindAsync(id);
            if (tipo == null || tipo.isDeleted)
            {
                return NotFound();
            }

            tipo.tipoMovimiento = tipoDto.tipoMovimiento;
            tipo.descripcion = tipoDto.descripcion;
            _context.TiposMovimiento.Update(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TiposMovimientoResponseDto>> PostTipoMovimiento(TiposMovimientoFormDto tipoDto)
        {
            var tipo = new TipoMovimiento
            {
                idTipoMovimiento = 0,
                tipoMovimiento = tipoDto.tipoMovimiento,
                descripcion = tipoDto.descripcion
            };

            _context.TiposMovimiento.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoMovimiento), new { id = tipo.idTipoMovimiento }, tipo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoMovimiento(int id)
        {
            var tipoMovimiento = await _context.TiposMovimiento.FindAsync(id);
            if (tipoMovimiento == null || tipoMovimiento.isDeleted)
            {
                return NotFound();
            }

            tipoMovimiento.isDeleted = true;
            _context.TiposMovimiento.Update(tipoMovimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoMovimientoExists(int id)
        {
            return _context.TiposMovimiento.Any(e => e.idTipoMovimiento == id);
        }
    }
}
