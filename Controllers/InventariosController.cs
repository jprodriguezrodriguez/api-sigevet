using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Inventarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public InventariosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventariosResponseDto>>> GetInventarios()
        {
            var inventarios = await _context.Inventarios
                .Include(i => i.insumoSanitario)
                .Where(i => !i.isDeleted)
                .Select(i => new InventariosResponseDto
                {
                    idInventario = i.idInventario,
                    cantidadDisponible = i.cantidadDisponible,
                    stockMinimo = i.stockMinimo,
                    insumoSanitario = i.insumoSanitario != null ? i.insumoSanitario.insumoSanitario : null
                }).ToListAsync();

            return Ok(inventarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventariosResponseDto>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios
                .Include(i => i.insumoSanitario)
                .Where(i => !i.isDeleted && i.idInventario == id)
                .Select(i => new InventariosResponseDto
                {
                    idInventario = i.idInventario,
                    cantidadDisponible = i.cantidadDisponible,
                    stockMinimo = i.stockMinimo,
                    insumoSanitario = i.insumoSanitario != null ? i.insumoSanitario.insumoSanitario : null
                }).FirstOrDefaultAsync();

            if (inventario == null)
            {
                return NotFound();
            }

            return Ok(inventario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(int id, [FromForm] InventariosFormDto inventarioDto)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null || inventario.isDeleted)
            {
                return NotFound();
            }

            inventario.cantidadDisponible = inventarioDto.cantidadDisponible;
            inventario.stockMinimo = inventarioDto.stockMinimo;
            inventario.idInsumoSanitario = inventarioDto.idInsumoSanitario;

            _context.Inventarios.Update(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<InventariosResponseDto>> PostInventario([FromForm] InventariosFormDto inventarioDto)
        {
            var inventario = new Inventario
            {
                cantidadDisponible = inventarioDto.cantidadDisponible,
                stockMinimo = inventarioDto.stockMinimo,
                idInsumoSanitario = inventarioDto.idInsumoSanitario
            };

            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventario), new { id = inventario.idInventario }, inventario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null || inventario.isDeleted)
            {
                return NotFound();
            }

            inventario.isDeleted = true;
            _context.Inventarios.Update(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
