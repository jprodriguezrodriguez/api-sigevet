using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.MovimientoInventarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoInventariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public MovimientoInventariosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventariosResponseDto>>> GetMovimientoInventario()
        {
            var movimientos = await _context.MovimientoInventario
                .Include(m => m.tipoMovimiento)
                .Include(m => m.responsableMovimiento)
                .Include(m => m.inventario)
                    .ThenInclude(i => i.insumoSanitario)
                .Include(m => m.brigada)
                .Where(m => !m.isDeleted)
                .Select(m => new MovimientoInventariosResponseDto
                {
                    idMovimientoInventario = m.idMovimientoInventario,
                    cantidad = m.cantidad,
                    fechaMovimiento = m.fechaMovimiento,
                    motivo = m.motivo,
                    observaciones = m.observaciones,
                    tipoMovimiento = m.tipoMovimiento != null ? m.tipoMovimiento.tipoMovimiento : null,
                    responsableMovimiento = m.responsableMovimiento != null ? m.responsableMovimiento.primerNombre + " " + m.responsableMovimiento.primerApellido : null,
                    insumoSanitario = m.inventario != null && m.inventario.insumoSanitario != null ? m.inventario.insumoSanitario.insumoSanitario : null,
                    brigada = m.brigada != null ? m.brigada.nombreBrigada : null
                }).ToListAsync();

            return Ok(movimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventariosResponseDto>> GetMovimientoInventario(int id)
        {
            var movimiento = await _context.MovimientoInventario
                .Include(m => m.tipoMovimiento)
                .Include(m => m.responsableMovimiento)
                .Include(m => m.inventario)
                    .ThenInclude(i => i.insumoSanitario)
                .Include(m => m.brigada)
                .Where(m => !m.isDeleted && m.idMovimientoInventario == id)
                .Select(m => new MovimientoInventariosResponseDto
                {
                    idMovimientoInventario = m.idMovimientoInventario,
                    cantidad = m.cantidad,
                    fechaMovimiento = m.fechaMovimiento,
                    motivo = m.motivo,
                    observaciones = m.observaciones,
                    tipoMovimiento = m.tipoMovimiento != null ? m.tipoMovimiento.tipoMovimiento : null,
                    responsableMovimiento = m.responsableMovimiento != null ? m.responsableMovimiento.primerNombre + " " + m.responsableMovimiento.primerApellido : null,
                    insumoSanitario = m.inventario != null && m.inventario.insumoSanitario != null ? m.inventario.insumoSanitario.insumoSanitario : null,
                    brigada = m.brigada != null ? m.brigada.nombreBrigada : null
                }).FirstOrDefaultAsync();

            if (movimiento == null)
            {
                return NotFound();
            }

            return Ok(movimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientoInventario(int id, MovimientoInventariosFormDto movimientoDto)
        {
            var movimiento = await _context.MovimientoInventario.FindAsync(id);
            if (movimiento == null || movimiento.isDeleted)
            {
                return NotFound();
            }

            movimiento.cantidad = movimientoDto.cantidad;
            movimiento.fechaMovimiento = movimientoDto.fechaMovimiento;
            movimiento.motivo = movimientoDto.motivo;
            movimiento.observaciones = movimientoDto.observaciones;
            movimiento.idTipoMovimiento = movimientoDto.idTipoMovimiento;
            movimiento.idResponsable = movimientoDto.idResponsable;
            movimiento.idInventario = movimientoDto.idInventario;
            movimiento.idBrigada = movimientoDto.idBrigada;

            _context.MovimientoInventario.Update(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MovimientoInventariosResponseDto>> PostMovimientoInventario(MovimientoInventariosFormDto movimientoDto)
        {
            var movimiento = new MovimientoInventario
            {
                cantidad = movimientoDto.cantidad,
                fechaMovimiento = movimientoDto.fechaMovimiento,
                motivo = movimientoDto.motivo,
                observaciones = movimientoDto.observaciones,
                idTipoMovimiento = movimientoDto.idTipoMovimiento,
                idResponsable = movimientoDto.idResponsable,
                idInventario = movimientoDto.idInventario,
                idBrigada = movimientoDto.idBrigada
            };

            _context.MovimientoInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimientoInventario), new { id = movimiento.idMovimientoInventario }, movimiento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientoInventario(int id)
        {
            var movimiento = await _context.MovimientoInventario.FindAsync(id);
            if (movimiento == null || movimiento.isDeleted)
            {
                return NotFound();
            }

            movimiento.isDeleted = true;
            _context.MovimientoInventario.Update(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoInventarioExists(int id)
        {
            return _context.MovimientoInventario.Any(e => e.idMovimientoInventario == id);
        }
    }
}
