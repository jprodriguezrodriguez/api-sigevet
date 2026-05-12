using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.TiposContacto;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposContactoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TiposContactoController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposContactoResponseDto>>> GetTiposContacto()
        {
            var tipos = await _context.TiposContacto
                .Where(t => !t.isDeleted)
                .Select(t => new TiposContactoResponseDto
                {
                    idTipoContacto = t.idTipoContacto,
                    tipoContacto = t.tipoContacto,
                    descripcion = t.descripcion
                }).ToListAsync();

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiposContactoResponseDto>> GetTipoContacto(int id)
        {
            var tipo = await _context.TiposContacto
                .Where(t => !t.isDeleted && t.idTipoContacto == id)
                .Select(t => new TiposContactoResponseDto
                {
                    idTipoContacto = t.idTipoContacto,
                    tipoContacto = t.tipoContacto,
                    descripcion = t.descripcion
                }).FirstOrDefaultAsync();

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoContacto(int id, TiposContactoFormDto tipoDto)
        {
            var tipo = await _context.TiposContacto.FindAsync(id);
            if (tipo == null || tipo.isDeleted)
            {
                return NotFound();
            }

            tipo.tipoContacto = tipoDto.tipoContacto;
            tipo.descripcion = tipoDto.descripcion;

            _context.TiposContacto.Update(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TiposContactoResponseDto>> PostTipoContacto(TiposContactoFormDto tipoDto)
        {
            var tipo = new TipoContacto
            {
                tipoContacto = tipoDto.tipoContacto,
                descripcion = tipoDto.descripcion
            };

            _context.TiposContacto.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoContacto), new { id = tipo.idTipoContacto }, new TiposContactoResponseDto
            {
                idTipoContacto = tipo.idTipoContacto,
                tipoContacto = tipo.tipoContacto,
                descripcion = tipo.descripcion
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoContacto(int id)
        {
            var tipoContacto = await _context.TiposContacto.FindAsync(id);
            if (tipoContacto == null || tipoContacto.isDeleted)
            {
                return NotFound();
            }

            tipoContacto.isDeleted = true;
            _context.TiposContacto.Update(tipoContacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
