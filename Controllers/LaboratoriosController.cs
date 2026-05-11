using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Laboratorios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoriosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public LaboratoriosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaboratoriosResponseDto>>> GetLaboratorios()
        {
            var laboratorios = await _context.Laboratorios
                .Where(l => !l.isDeleted)
                .Select(l => new LaboratoriosResponseDto
                {
                    idLaboratorio = l.idLaboratorio,
                    laboratorio = l.laboratorio,
                    descripcion = l.descripcion,
                    direccion = l.direccion
                }).ToListAsync();

            return Ok(laboratorios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LaboratoriosResponseDto>> GetLaboratorio(int id)
        {
            var laboratorio = await _context.Laboratorios
                .Where(l => !l.isDeleted && l.idLaboratorio == id)
                .Select(l => new LaboratoriosResponseDto
                {
                    idLaboratorio = l.idLaboratorio,
                    laboratorio = l.laboratorio,
                    descripcion = l.descripcion,
                    direccion = l.direccion
                }).FirstOrDefaultAsync();

            if (laboratorio == null)
            {
                return NotFound();
            }

            return Ok(laboratorio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratorio(int id, LaboratoriosFormDto laboratorioDto)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null || laboratorio.isDeleted)
            {
                return NotFound();
            }

            laboratorio.laboratorio = laboratorioDto.laboratorio;
            laboratorio.descripcion = laboratorioDto.descripcion;
            laboratorio.direccion = laboratorioDto.direccion;

            _context.Laboratorios.Update(laboratorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<LaboratoriosResponseDto>> PostLaboratorio(LaboratoriosFormDto laboratorioDto)
        {
            var laboratorio = new Laboratorio
            {
                laboratorio = laboratorioDto.laboratorio,
                descripcion = laboratorioDto.descripcion,
                direccion = laboratorioDto.direccion
            };

            _context.Laboratorios.Add(laboratorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLaboratorio), new { id = laboratorio.idLaboratorio }, laboratorio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratorio(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null || laboratorio.isDeleted)
            {
                return NotFound();
            }

            laboratorio.isDeleted = true;
            _context.Laboratorios.Update(laboratorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LaboratorioExists(int id)
        {
            return _context.Laboratorios.Any(e => e.idLaboratorio == id);
        }
    }
}
