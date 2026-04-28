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
    public class CategoriasEstadoController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public CategoriasEstadoController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoriasEstado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaEstado>>> GetCategoriasEstado()
        {
            return await _context.CategoriasEstado.ToListAsync();
        }

        // GET: api/CategoriaEstadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEstado>> GetCategoriaEstado(int id)
        {
            var categoriaEstado = await _context.CategoriasEstado.FindAsync(id);

            if (categoriaEstado == null)
            {
                return NotFound();
            }

            return categoriaEstado;
        }

        // PUT: api/CategoriaEstadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEstado(int id, CategoriaEstado categoriaEstado)
        {
            if (id != categoriaEstado.idCategoriaEstado)
            {
                return BadRequest();
            }

            _context.Entry(categoriaEstado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaEstadoExists(id))
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

        // POST: api/CategoriaEstadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaEstado>> PostCategoriaEstado(CategoriaEstado categoriaEstado)
        {
            _context.CategoriasEstado.Add(categoriaEstado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaEstado", new { id = categoriaEstado.idCategoriaEstado }, categoriaEstado);
        }

        //// DELETE: api/CategoriaEstadoes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategoriaEstado(int id)
        //{
        //    var categoriaEstado = await _context.CategoriasEstado.FindAsync(id);
        //    if (categoriaEstado == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CategoriasEstado.Remove(categoriaEstado);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // GET: api/CategoriasEstado/2/estados
        [HttpGet("estados-por-categoria/{id}")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstadosDeCategoria(int id)
        {
            var categoriaExiste = await _context.CategoriasEstado
                .AnyAsync(c => c.idCategoriaEstado == id);

            if (!categoriaExiste)
            {
                return NotFound($"No existe una categoría de estado con ID {id}.");
            }

            var estados = await _context.Estados
                .Where(e => e.idCategoriaEstado == id)
                .ToListAsync();

            return Ok(estados);
        }

        private bool CategoriaEstadoExists(int id)
        {
            return _context.CategoriasEstado.Any(e => e.idCategoriaEstado == id);
        }
    }
}
