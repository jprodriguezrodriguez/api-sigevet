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
    public class EstadosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EstadosController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await _context.Estados.ToListAsync();
        }

        // GET: api/Estados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            if (id != estado.idEstado)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.idEstado }, estado);
        }

        // DELETE: api/Estados/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEstado(int id)
        //{
        //    var estado = await _context.Estados.FindAsync(id);
        //    if (estado == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Estados.Remove(estado);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // GET: api/Estados/por-categoria/2
        [HttpGet("por-categoria/{idCategoriaEstado}")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstadosPorCategoria(int idCategoriaEstado)
        {
            var categoriaExiste = await _context.CategoriasEstado
                .AnyAsync(c => c.idCategoriaEstado == idCategoriaEstado);

            if (!categoriaExiste)
            {
                return NotFound($"No existe una categoría de estado con ID {idCategoriaEstado}.");
            }

            var estados = await _context.Estados
                .Where(e => e.idCategoriaEstado == idCategoriaEstado)
                .ToListAsync();

            return Ok(estados);
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.idEstado == id);
        }
    }
}
