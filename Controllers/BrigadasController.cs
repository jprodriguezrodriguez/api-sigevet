using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Brigadas;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrigadasController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public BrigadasController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrigadasResponseDto>>> GetBrigadas()
        {
            var brigadas = await _context.Brigadas
                .Include(b => b.estadoBrigada)
                .Where(b => !b.isDeleted)
                .Select(b => new BrigadasResponseDto
                {
                    idBrigada = b.idBrigada,
                    nombreBrigada = b.nombreBrigada,
                    fechaBrigada = b.fechaBrigada,
                    horaInicio = b.horaInicio,
                    horaFin = b.horaFin,
                    ubicacion = b.ubicacion,
                    cobertura = b.cobertura,
                    observaciones = b.observaciones,
                    estadoBrigada = b.estadoBrigada != null ? b.estadoBrigada.estado : null
                }).ToListAsync();

            return Ok(brigadas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrigadasResponseDto>> GetBrigada(int id)
        {
            var brigada = await _context.Brigadas
                .Include(b => b.estadoBrigada)
                .Where(b => !b.isDeleted && b.idBrigada == id)
                .Select(b => new BrigadasResponseDto
                {
                    idBrigada = b.idBrigada,
                    nombreBrigada = b.nombreBrigada,
                    fechaBrigada = b.fechaBrigada,
                    horaInicio = b.horaInicio,
                    horaFin = b.horaFin,
                    ubicacion = b.ubicacion,
                    cobertura = b.cobertura,
                    observaciones = b.observaciones,
                    estadoBrigada = b.estadoBrigada != null ? b.estadoBrigada.estado : null
                }).FirstOrDefaultAsync();

            if (brigada == null)
            {
                return NotFound();
            }

            return Ok(brigada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrigada(int id, BrigadasFormDto brigadaDto)
        {
            var brigada = await _context.Brigadas.FindAsync(id);
            if (brigada == null || brigada.isDeleted)
            {
                return NotFound();
            }

            brigada.nombreBrigada = brigadaDto.nombreBrigada;
            brigada.fechaBrigada = brigadaDto.fechaBrigada;
            brigada.horaInicio = brigadaDto.horaInicio;
            brigada.horaFin = brigadaDto.horaFin;
            brigada.ubicacion = brigadaDto.ubicacion;
            brigada.cobertura = brigadaDto.cobertura;
            brigada.observaciones = brigadaDto.observaciones;
            brigada.idEstadoBrigada = brigadaDto.idEstadoBrigada;

            _context.Brigadas.Update(brigada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BrigadasResponseDto>> PostBrigada(BrigadasFormDto brigadaDto)
        {
            var brigada = new Brigada
            {
                nombreBrigada = brigadaDto.nombreBrigada,
                fechaBrigada = brigadaDto.fechaBrigada,
                horaInicio = brigadaDto.horaInicio,
                horaFin = brigadaDto.horaFin,
                ubicacion = brigadaDto.ubicacion,
                cobertura = brigadaDto.cobertura,
                observaciones = brigadaDto.observaciones,
                idEstadoBrigada = brigadaDto.idEstadoBrigada
            };

            _context.Brigadas.Add(brigada);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrigada), new { id = brigada.idBrigada }, brigada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrigada(int id)
        {
            var brigada = await _context.Brigadas.FindAsync(id);
            if (brigada == null || brigada.isDeleted)
            {
                return NotFound();
            }

            brigada.isDeleted = true;
            _context.Brigadas.Update(brigada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrigadaExists(int id)
        {
            return _context.Brigadas.Any(e => e.idBrigada == id);
        }
    }
}
