using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Especies;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspeciesController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspeciesResponseDto>>> GetEspecies()
        {
            var especies = await _context.Especies
                .Where(e => !e.isDeleted)
                .Select(e => new EspeciesResponseDto
                {
                    idEspecie = e.idEspecie,
                    especie = e.especie,
                    descripcion = e.descripcion
                }).ToListAsync();

            return Ok(especies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspeciesResponseDto>> GetEspecie(int id)
        {
            var especie = await _context.Especies
                .Where(e => !e.isDeleted && e.idEspecie == id)
                .Select(e => new EspeciesResponseDto
                {
                    idEspecie = e.idEspecie,
                    especie = e.especie,
                    descripcion = e.descripcion
                }).FirstOrDefaultAsync();

            if (especie == null)
            {
                return NotFound();
            }

            return Ok(especie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecie(int id, EspeciesFormDto especieDto)
        {
            var especie = await _context.Especies.FindAsync(id);
            if (especie == null || especie.isDeleted)
            {
                return NotFound();
            }

            especie.especie = especieDto.especie;
            especie.descripcion = especieDto.descripcion;

            _context.Especies.Update(especie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EspeciesResponseDto>> PostEspecie(EspeciesFormDto especieDto)
        {
            var especie = new Especie
            {
                idEspecie = 0,
                especie = especieDto.especie,
                descripcion = especieDto.descripcion
            };

            _context.Especies.Add(especie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspecie), new { id = especie.idEspecie }, especie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecie(int id)
        {
            var especie = await _context.Especies.FindAsync(id);
            if (especie == null || especie.isDeleted)
            {
                return NotFound();
            }

            especie.isDeleted = true;
            _context.Especies.Update(especie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecieExists(int id)
        {
            return _context.Especies.Any(e => e.idEspecie == id);
        }
    }
}
