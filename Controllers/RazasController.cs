using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Razas;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public RazasController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RazasResponseDto>>> GetRazas()
        {
            var razas = await _context.Razas
                .Include(r => r.especie)
                .Where(r => !r.isDeleted)
                .Select(r => new RazasResponseDto
                {
                    idRaza = r.idRaza,
                    raza = r.raza,
                    descripcion = r.descripcion,
                    especie = r.especie.especie
                }).ToListAsync();

            return Ok(razas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RazasResponseDto>> GetRaza(int id)
        {
            var raza = await _context.Razas
                .Include(r => r.especie)
                .Where(r => !r.isDeleted && r.idRaza == id)
                .Select(r => new RazasResponseDto
                {
                    idRaza = r.idRaza,
                    raza = r.raza,
                    descripcion = r.descripcion,
                    especie = r.especie.especie
                }).FirstOrDefaultAsync();

            if (raza == null)
            {
                return NotFound();
            }

            return Ok(raza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaza(int id, RazasFormDto razaDto)
        {
            var raza = await _context.Razas.FindAsync(id);
            if (raza == null || raza.isDeleted)
            {
                return NotFound();
            }

            raza.raza = razaDto.raza;
            raza.descripcion = razaDto.descripcion;
            raza.idEspecie = razaDto.idEspecie;

            _context.Razas.Update(raza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<RazasResponseDto>> PostRaza(RazasFormDto razaDto)
        {
            var raza = new Raza
            {
                idRaza = 0,
                raza = razaDto.raza,
                descripcion = razaDto.descripcion,
                idEspecie = razaDto.idEspecie
            };

            _context.Razas.Add(raza);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRaza), new { id = raza.idRaza }, raza);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaza(int id)
        {
            var raza = await _context.Razas.FindAsync(id);
            if (raza == null || raza.isDeleted)
            {
                return NotFound();
            }

            raza.isDeleted = true;
            _context.Razas.Update(raza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("razas-por-especie/{id}")]
        public async Task<ActionResult<IEnumerable<RazasResponseDto>>> GetRazasPorEspecie(int id)
        {
            var especieExiste = await _context.Especies.AnyAsync(e => e.idEspecie == id && !e.isDeleted);
            if (!especieExiste)
            {
                return NotFound($"No existe una especie con ID {id}.");
            }

            var razas = await _context.Razas
                .Include(r => r.especie)
                .Where(r => r.idEspecie == id && !r.isDeleted)
                .Select(r => new RazasResponseDto
                {
                    idRaza = r.idRaza,
                    raza = r.raza,
                    descripcion = r.descripcion,
                    especie = r.especie.especie
                }).ToListAsync();

            return Ok(razas);
        }
    }
}
