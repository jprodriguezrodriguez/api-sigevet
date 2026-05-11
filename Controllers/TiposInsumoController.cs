using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.TiposInsumo;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposInsumoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposInsumoController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposInsumoResponseDto>>> GetTiposInsumo()
        {
            var tipos = await _context.TiposInsumo
                .Where(t => !t.isDeleted)
                .Select(t => new TiposInsumoResponseDto
                {
                    idTipoInsumo = t.idTipoInsumo,
                    tipoInsumo = t.tipoInsumo,
                    descripcion = t.descripcion
                }).ToListAsync();

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiposInsumoResponseDto>> GetTipoInsumo(int id)
        {
            var tipo = await _context.TiposInsumo
                .Where(t => !t.isDeleted && t.idTipoInsumo == id)
                .Select(t => new TiposInsumoResponseDto
                {
                    idTipoInsumo = t.idTipoInsumo,
                    tipoInsumo = t.tipoInsumo,
                    descripcion = t.descripcion
                }).FirstOrDefaultAsync();

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoInsumo(int id, TiposInsumoFormDto tipoDto)
        {
            var tipo = await _context.TiposInsumo.FindAsync(id);
            if (tipo == null || tipo.isDeleted)
            {
                return NotFound();
            }

            tipo.tipoInsumo = tipoDto.tipoInsumo;
            tipo.descripcion = tipoDto.descripcion;
            _context.TiposInsumo.Update(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TiposInsumoResponseDto>> PostTipoInsumo(TiposInsumoFormDto tipoDto)
        {
            var tipo = new TipoInsumo
            {
                tipoInsumo = tipoDto.tipoInsumo,
                descripcion = tipoDto.descripcion
            };

            _context.TiposInsumo.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoInsumo), new { id = tipo.idTipoInsumo }, tipo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoInsumo(int id)
        {
            var tipoInsumo = await _context.TiposInsumo.FindAsync(id);
            if (tipoInsumo == null || tipoInsumo.isDeleted)
            {
                return NotFound();
            }

            tipoInsumo.isDeleted = true;
            _context.TiposInsumo.Update(tipoInsumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoInsumoExists(int id)
        {
            return _context.TiposInsumo.Any(e => e.idTipoInsumo == id);
        }
    }
}
