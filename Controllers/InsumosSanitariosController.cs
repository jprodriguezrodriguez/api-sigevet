using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.InsumosSanitarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsumosSanitariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public InsumosSanitariosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsumosSanitariosResponseDto>>> GetInsumosSanitarios()
        {
            var insumos = await _context.InsumosSanitarios
                .Include(i => i.tipoInsumo)
                .Include(i => i.unidadMedida)
                .Include(i => i.estadoInsumo)
                .Where(i => !i.isDeleted)
                .Select(i => new InsumosSanitariosResponseDto
                {
                    idInsumoSanitario = i.idInsumoSanitario,
                    insumoSanitario = i.insumoSanitario,
                    descripcion = i.descripcion,
                    tipoInsumo = i.tipoInsumo != null ? i.tipoInsumo.tipoInsumo : null,
                    unidadMedida = i.unidadMedida != null ? i.unidadMedida.unidadMedida : null,
                    estadoInsumo = i.estadoInsumo != null ? i.estadoInsumo.estado : null
                }).ToListAsync();

            return Ok(insumos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InsumosSanitariosResponseDto>> GetInsumoSanitario(int id)
        {
            var insumo = await _context.InsumosSanitarios
                .Include(i => i.tipoInsumo)
                .Include(i => i.unidadMedida)
                .Include(i => i.estadoInsumo)
                .Where(i => !i.isDeleted && i.idInsumoSanitario == id)
                .Select(i => new InsumosSanitariosResponseDto
                {
                    idInsumoSanitario = i.idInsumoSanitario,
                    insumoSanitario = i.insumoSanitario,
                    descripcion = i.descripcion,
                    tipoInsumo = i.tipoInsumo != null ? i.tipoInsumo.tipoInsumo : null,
                    unidadMedida = i.unidadMedida != null ? i.unidadMedida.unidadMedida : null,
                    estadoInsumo = i.estadoInsumo != null ? i.estadoInsumo.estado : null
                }).FirstOrDefaultAsync();

            if (insumo == null)
            {
                return NotFound();
            }

            return Ok(insumo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumoSanitario(int id, [FromForm] InsumosSanitariosFormDto insumoDto)
        {
            var insumo = await _context.InsumosSanitarios.FindAsync(id);
            if (insumo == null || insumo.isDeleted)
            {
                return NotFound();
            }

            insumo.insumoSanitario = insumoDto.insumoSanitario;
            insumo.descripcion = insumoDto.descripcion;
            insumo.idTipoInsumo = insumoDto.idTipoInsumo;
            insumo.idUnidadMedida = insumoDto.idUnidadMedida;
            insumo.idEstadoInsumo = insumoDto.idEstadoInsumo;

            _context.InsumosSanitarios.Update(insumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<InsumosSanitariosResponseDto>> PostInsumoSanitario([FromForm] InsumosSanitariosFormDto insumoDto)
        {
            var insumo = new InsumoSanitario
            {
                insumoSanitario = insumoDto.insumoSanitario,
                descripcion = insumoDto.descripcion,
                idTipoInsumo = insumoDto.idTipoInsumo,
                idUnidadMedida = insumoDto.idUnidadMedida,
                idEstadoInsumo = insumoDto.idEstadoInsumo
            };

            _context.InsumosSanitarios.Add(insumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsumoSanitario), new { id = insumo.idInsumoSanitario }, insumo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsumoSanitario(int id)
        {
            var insumoSanitario = await _context.InsumosSanitarios.FindAsync(id);
            if (insumoSanitario == null || insumoSanitario.isDeleted)
            {
                return NotFound();
            }

            insumoSanitario.isDeleted = true;
            _context.InsumosSanitarios.Update(insumoSanitario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsumoSanitarioExists(int id)
        {
            return _context.InsumosSanitarios.Any(e => e.idInsumoSanitario == id);
        }
    }
}
