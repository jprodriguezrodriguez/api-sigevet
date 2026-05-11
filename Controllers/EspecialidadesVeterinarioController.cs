using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.EspecialidadesVeterinario;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesVeterinarioController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspecialidadesVeterinarioController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadesVeterinarioResponseDto>>> GetEspecialidadesVeterinario()
        {
            var registros = await _context.EspecialidadesVeterinario
                .Include(e => e.veterinarios)
                    .ThenInclude(v => v!.persona)
                .Include(e => e.especialidad)
                .Where(e => !e.isDeleted)
                .Select(e => new EspecialidadesVeterinarioResponseDto
                {
                    idVeterinarioEspecialidad = e.idVeterinarioEspecialidad,
                    veterinario = e.veterinarios != null && e.veterinarios.persona != null ? e.veterinarios.persona.primerNombre + " " + e.veterinarios.persona.primerApellido : null,
                    especialidad = e.especialidad != null ? e.especialidad.especialidad : null
                }).ToListAsync();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadesVeterinarioResponseDto>> GetEspecialidadVeterinario(int id)
        {
            var registro = await _context.EspecialidadesVeterinario
                .Include(e => e.veterinarios)
                    .ThenInclude(v => v!.persona)
                .Include(e => e.especialidad)
                .Where(e => !e.isDeleted && e.idVeterinarioEspecialidad == id)
                .Select(e => new EspecialidadesVeterinarioResponseDto
                {
                    idVeterinarioEspecialidad = e.idVeterinarioEspecialidad,
                    veterinario = e.veterinarios != null && e.veterinarios.persona != null ? e.veterinarios.persona.primerNombre + " " + e.veterinarios.persona.primerApellido : null,
                    especialidad = e.especialidad != null ? e.especialidad.especialidad : null
                }).FirstOrDefaultAsync();

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidadVeterinario(int id, EspecialidadesVeterinarioFormDto registroDto)
        {
            var registro = await _context.EspecialidadesVeterinario.FindAsync(id);
            if (registro == null || registro.isDeleted)
            {
                return NotFound();
            }

            registro.idVeterinario = registroDto.idVeterinario;
            registro.idEspecialidad = registroDto.idEspecialidad;

            _context.EspecialidadesVeterinario.Update(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EspecialidadesVeterinarioResponseDto>> PostEspecialidadVeterinario(EspecialidadesVeterinarioFormDto registroDto)
        {
            var registro = new EspecialidadVeterinario
            {
                idVeterinario = registroDto.idVeterinario,
                idEspecialidad = registroDto.idEspecialidad
            };

            _context.EspecialidadesVeterinario.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspecialidadVeterinario), new { id = registro.idVeterinarioEspecialidad }, registro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidadVeterinario(int id)
        {
            var registro = await _context.EspecialidadesVeterinario.FindAsync(id);
            if (registro == null || registro.isDeleted)
            {
                return NotFound();
            }

            registro.isDeleted = true;
            _context.EspecialidadesVeterinario.Update(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecialidadVeterinarioExists(int id)
        {
            return _context.EspecialidadesVeterinario.Any(e => e.idVeterinarioEspecialidad == id);
        }
    }
}
