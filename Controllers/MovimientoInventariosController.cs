using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: api/MovimientoInventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientoInventario()
        {
            return await _context.MovimientoInventario.ToListAsync();
        }

        // GET: api/MovimientoInventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimientoInventario(int id)
        {
            var movimientoInventario = await _context.MovimientoInventario.FindAsync(id);

            if (movimientoInventario == null)
            {
                return NotFound();
            }

            return movimientoInventario;
        }

        // PUT: api/MovimientoInventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovimientoInventario(int id, MovimientoInventario movimientoInventario)
        //{
        //    if (id != movimientoInventario.idMovimientoInventario)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movimientoInventario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovimientoInventarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/MovimientoInventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostMovimientoInventario(MovimientoInventario movimientoInventario)
        {
            if (movimientoInventario.cantidad <= 0)
            {
                return BadRequest("La cantidad del movimiento debe ser mayor a cero.");
            }

            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.idInventario == movimientoInventario.idInventario);

            if (inventario == null)
            {
                return NotFound($"No existe un inventario con ID {movimientoInventario.idInventario}.");
            }

            var tipoMovimientoExiste = await _context.TiposMovimiento
                .AnyAsync(t => t.idTipoMovimiento == movimientoInventario.idTipoMovimiento);

            if (!tipoMovimientoExiste)
            {
                return NotFound($"No existe un tipo de movimiento con ID {movimientoInventario.idTipoMovimiento}.");
            }

            var responsableExiste = await _context.Personas
                .AnyAsync(p => p.idPersona == movimientoInventario.idResponsable);

            if (!responsableExiste)
            {
                return NotFound($"No existe una persona responsable con ID {movimientoInventario.idResponsable}.");
            }

            var brigadaExiste = await _context.Brigadas
                .AnyAsync(b => b.idBrigada == movimientoInventario.idBrigada);

            if (!brigadaExiste)
            {
                return NotFound($"No existe una brigada con ID {movimientoInventario.idBrigada}.");
            }

            switch (movimientoInventario.idTipoMovimiento)
            {
                case 1: // Entrada
                case 3: // Ajuste positivo
                    inventario.cantidadDisponible += movimientoInventario.cantidad;
                    break;

                case 2: // Salida
                case 4: // Ajuste negativo
                    if (inventario.cantidadDisponible < movimientoInventario.cantidad)
                    {
                        return BadRequest(
                            $"No hay stock suficiente. Disponible: {inventario.cantidadDisponible}, solicitado: {movimientoInventario.cantidad}."
                        );
                    }

                    inventario.cantidadDisponible -= movimientoInventario.cantidad;
                    break;

                default:
                    return BadRequest("Tipo de movimiento no válido.");
            }

            movimientoInventario.fechaCreacion = DateTime.Now;
            movimientoInventario.fechaActualizacion = DateTime.Now;
            inventario.fechaActualizacion = DateTime.Now;

            _context.MovimientoInventario.Add(movimientoInventario);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMovimientoInventario),
                new { id = movimientoInventario.idMovimientoInventario },
                new
                {
                    movimientoInventario.idMovimientoInventario,
                    movimientoInventario.cantidad,
                    movimientoInventario.fechaMovimiento,
                    movimientoInventario.motivo,
                    movimientoInventario.observaciones,
                    movimientoInventario.idTipoMovimiento,
                    movimientoInventario.idResponsable,
                    movimientoInventario.idInventario,
                    movimientoInventario.idBrigada,
                    cantidadDisponibleActualizada = inventario.cantidadDisponible
                }
            );
        }

        // DELETE: api/MovimientoInventarios/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovimientoInventario(int id)
        //{
        //    var movimientoInventario = await _context.MovimientoInventario.FindAsync(id);
        //    if (movimientoInventario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MovimientoInventario.Remove(movimientoInventario);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool MovimientoInventarioExists(int id)
        {
            return _context.MovimientoInventario.Any(e => e.idMovimientoInventario == id);
        }
    }
}
