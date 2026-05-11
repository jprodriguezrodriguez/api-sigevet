using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.BrigadasVeterinarios;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrigadasVeterinariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public BrigadasVeterinariosController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrigadasVeterinariosResponseDto>>> GetBrigadasVeterinario()
        {
            var registros = await _context.BrigadasVeterinario
                .Include(b => b.veterinario)
                    .ThenInclude(v => v!.persona)
                .Include(b => b.brigadas)
                .Include(b => b.rolParticipacion)
                .Where(b => !b.isDeleted)
                .Select(b => new BrigadasVeterinariosResponseDto
                {
                    idBrigadaVeterinario = b.idBrigadaVeterinario,
                    veterinario = b.veterinario != null && b.veterinario.persona != null ? b.veterinario.persona.primerNombre + " " + b.veterinario.persona.primerApellido : null,
                    brigada = b.brigadas != null ? b.brigadas.nombreBrigada : null,
                    rolParticipacion = b.rolParticipacion != null ? b.rolParticipacion.rolParticipacion : null
                }).ToListAsync();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrigadasVeterinariosResponseDto>> GetBrigadaVeterinario(int id)
        {
            var registro = await _context.BrigadasVeterinario
                .Include(b => b.veterinario)
                    .ThenInclude(v => v!.persona)
                .Include(b => b.brigadas)
                .Include(b => b.rolParticipacion)
                .Where(b => !b.isDeleted && b.idBrigadaVeterinario == id)
                .Select(b => new BrigadasVeterinariosResponseDto
                {
                    idBrigadaVeterinario = b.idBrigadaVeterinario,
                    veterinario = b.veterinario != null && b.veterinario.persona != null ? b.veterinario.persona.primerNombre + " " + b.veterinario.persona.primerApellido : null,
                    brigada = b.brigadas != null ? b.brigadas.nombreBrigada : null,
                    rolParticipacion = b.rolParticipacion != null ? b.rolParticipacion.rolParticipacion : null
                }).FirstOrDefaultAsync();

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrigadaVeterinario(int id, BrigadasVeterinariosFormDto registroDto)
        {
            var registro = await _context.BrigadasVeterinario.FindAsync(id);
            if (registro == null || registro.isDeleted)
            {
                return NotFound();
            }

            registro.idVeterinario = registroDto.idVeterinario;
            registro.idBrigada = registroDto.idBrigada;
            registro.idRolParticipacion = registroDto.idRolParticipacion;

            _context.BrigadasVeterinario.Update(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BrigadasVeterinariosResponseDto>> PostBrigadaVeterinario(BrigadasVeterinariosFormDto registroDto)
        {
            var registro = new BrigadaVeterinario
            {
                idVeterinario = registroDto.idVeterinario,
                idBrigada = registroDto.idBrigada,
                idRolParticipacion = registroDto.idRolParticipacion
            };

            _context.BrigadasVeterinario.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrigadaVeterinario), new { id = registro.idBrigadaVeterinario }, registro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrigadaVeterinario(int id)
        {
            var registro = await _context.BrigadasVeterinario.FindAsync(id);
            if (registro == null || registro.isDeleted)
            {
                return NotFound();
            }

            registro.isDeleted = true;
            _context.BrigadasVeterinario.Update(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrigadaVeterinarioExists(int id)
        {
            return _context.BrigadasVeterinario.Any(e => e.idBrigadaVeterinario == id);
        }
    }
}
