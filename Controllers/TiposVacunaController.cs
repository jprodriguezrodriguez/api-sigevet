using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.TiposVacuna;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposVacunaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposVacunaController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposVacunaResponseDto>>> GetTiposVacuna()
        {
            var tipos = await _context.TiposVacuna
                .Where(t => !t.isDeleted)
                .Select(t => new TiposVacunaResponseDto
                {
                    idTipoVacuna = t.idTipoVacuna,
                    tipoVacuna = t.tipoVacuna,
                    descripcion = t.descripcion
                }).ToListAsync();

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiposVacunaResponseDto>> GetTipoVacuna(int id)
        {
            var tipo = await _context.TiposVacuna
                .Where(t => !t.isDeleted && t.idTipoVacuna == id)
                .Select(t => new TiposVacunaResponseDto
                {
                    idTipoVacuna = t.idTipoVacuna,
                    tipoVacuna = t.tipoVacuna,
                    descripcion = t.descripcion
                }).FirstOrDefaultAsync();

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoVacuna(int id, [FromForm] TiposVacunaFormDto tipoDto)
        {
            var tipo = await _context.TiposVacuna.FindAsync(id);
            if (tipo == null || tipo.isDeleted)
            {
                return NotFound();
            }

            tipo.tipoVacuna = tipoDto.tipoVacuna;
            tipo.descripcion = tipoDto.descripcion;

            _context.TiposVacuna.Update(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TiposVacunaResponseDto>> PostTipoVacuna([FromForm] TiposVacunaFormDto tipoDto)
        {
            var tipo = new TipoVacuna
            {
                idTipoVacuna = 0,
                tipoVacuna = tipoDto.tipoVacuna,
                descripcion = tipoDto.descripcion
            };

            _context.TiposVacuna.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoVacuna), new { id = tipo.idTipoVacuna }, tipo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoVacuna(int id)
        {
            var tipoVacuna = await _context.TiposVacuna.FindAsync(id);
            if (tipoVacuna == null || tipoVacuna.isDeleted)
            {
                return NotFound();
            }

            tipoVacuna.isDeleted = true;
            _context.TiposVacuna.Update(tipoVacuna);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
