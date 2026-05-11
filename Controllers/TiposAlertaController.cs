using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.TiposAlerta;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposAlertaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposAlertaController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposAlertaResponseDto>>> GetTiposAlerta()
        {
            var tipos = await _context.TiposAlerta
                .Where(t => !t.isDeleted)
                .Select(t => new TiposAlertaResponseDto
                {
                    idTipoAlerta = t.idTipoAlerta,
                    alerta = t.alerta,
                    descripcion = t.descripcion
                }).ToListAsync();

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiposAlertaResponseDto>> GetTipoAlerta(int id)
        {
            var tipo = await _context.TiposAlerta
                .Where(t => !t.isDeleted && t.idTipoAlerta == id)
                .Select(t => new TiposAlertaResponseDto
                {
                    idTipoAlerta = t.idTipoAlerta,
                    alerta = t.alerta,
                    descripcion = t.descripcion
                }).FirstOrDefaultAsync();

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAlerta(int id, TiposAlertaFormDto tipoDto)
        {
            var tipo = await _context.TiposAlerta.FindAsync(id);
            if (tipo == null || tipo.isDeleted)
            {
                return NotFound();
            }

            tipo.alerta = tipoDto.alerta;
            tipo.descripcion = tipoDto.descripcion;
            _context.TiposAlerta.Update(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TiposAlertaResponseDto>> PostTipoAlerta(TiposAlertaFormDto tipoDto)
        {
            var tipo = new TipoAlerta
            {
                alerta = tipoDto.alerta,
                descripcion = tipoDto.descripcion
            };

            _context.TiposAlerta.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoAlerta), new { id = tipo.idTipoAlerta }, tipo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoAlerta(int id)
        {
            var tipoAlerta = await _context.TiposAlerta.FindAsync(id);
            if (tipoAlerta == null || tipoAlerta.isDeleted)
            {
                return NotFound();
            }

            tipoAlerta.isDeleted = true;
            _context.TiposAlerta.Update(tipoAlerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoAlertaExists(int id)
        {
            return _context.TiposAlerta.Any(e => e.idTipoAlerta == id);
        }
    }
}
