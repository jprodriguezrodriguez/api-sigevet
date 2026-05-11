using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Vacunas;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacunasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public VacunasController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacunasResponseDto>>> GetVacunas()
        {
            var vacunas = await _context.Vacunas
                .Include(v => v.tipoVacuna)
                .Include(v => v.laboratorio)
                .Include(v => v.estadoVacuna)
                .Where(v => !v.isDeleted)
                .Select(v => new VacunasResponseDto
                {
                    idVacuna = v.idVacuna,
                    nombre = v.nombre,
                    numeroLote = v.numeroLote,
                    fechaFabricacion = v.fechaFabricacion,
                    fechaVencimiento = v.fechaVencimiento,
                    tipoVacuna = v.tipoVacuna != null ? v.tipoVacuna.tipoVacuna : null,
                    laboratorio = v.laboratorio != null ? v.laboratorio.laboratorio : null,
                    estadoVacuna = v.estadoVacuna != null ? v.estadoVacuna.estado : null
                }).ToListAsync();

            return Ok(vacunas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacunasResponseDto>> GetVacuna(int id)
        {
            var vacuna = await _context.Vacunas
                .Include(v => v.tipoVacuna)
                .Include(v => v.laboratorio)
                .Include(v => v.estadoVacuna)
                .Where(v => !v.isDeleted && v.idVacuna == id)
                .Select(v => new VacunasResponseDto
                {
                    idVacuna = v.idVacuna,
                    nombre = v.nombre,
                    numeroLote = v.numeroLote,
                    fechaFabricacion = v.fechaFabricacion,
                    fechaVencimiento = v.fechaVencimiento,
                    tipoVacuna = v.tipoVacuna != null ? v.tipoVacuna.tipoVacuna : null,
                    laboratorio = v.laboratorio != null ? v.laboratorio.laboratorio : null,
                    estadoVacuna = v.estadoVacuna != null ? v.estadoVacuna.estado : null
                }).FirstOrDefaultAsync();

            if (vacuna == null)
            {
                return NotFound();
            }

            return Ok(vacuna);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacuna(int id, VacunasFormDto vacunaDto)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null || vacuna.isDeleted)
            {
                return NotFound();
            }

            vacuna.nombre = vacunaDto.nombre;
            vacuna.numeroLote = vacunaDto.numeroLote;
            vacuna.fechaFabricacion = vacunaDto.fechaFabricacion;
            vacuna.fechaVencimiento = vacunaDto.fechaVencimiento;
            vacuna.idTipoVacuna = vacunaDto.idTipoVacuna;
            vacuna.idLaboratorio = vacunaDto.idLaboratorio;
            vacuna.idEstadoVacuna = vacunaDto.idEstadoVacuna;

            _context.Vacunas.Update(vacuna);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<VacunasResponseDto>> PostVacuna(VacunasFormDto vacunaDto)
        {
            var vacuna = new Vacuna
            {
                nombre = vacunaDto.nombre,
                numeroLote = vacunaDto.numeroLote,
                fechaFabricacion = vacunaDto.fechaFabricacion,
                fechaVencimiento = vacunaDto.fechaVencimiento,
                idTipoVacuna = vacunaDto.idTipoVacuna,
                idLaboratorio = vacunaDto.idLaboratorio,
                idEstadoVacuna = vacunaDto.idEstadoVacuna
            };

            _context.Vacunas.Add(vacuna);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacuna), new { id = vacuna.idVacuna }, vacuna);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacuna(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null || vacuna.isDeleted)
            {
                return NotFound();
            }

            vacuna.isDeleted = true;
            _context.Vacunas.Update(vacuna);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacunaExists(int id)
        {
            return _context.Vacunas.Any(e => e.idVacuna == id);
        }
    }
}
