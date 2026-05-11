using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Especialidades;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public EspecialidadesController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadesResponseDto>>> GetEspecialidades()
        {
            var especialidades = await _context.Especialidades
                .Where(e => !e.isDeleted)
                .Select(e => new EspecialidadesResponseDto
                {
                    idEspecialidad = e.idEspecialidad,
                    especialidad = e.especialidad,
                    descripcion = e.descripcion
                }).ToListAsync();

            return Ok(especialidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadesResponseDto>> GetEspecialidad(int id)
        {
            var especialidad = await _context.Especialidades
                .Where(e => !e.isDeleted && e.idEspecialidad == id)
                .Select(e => new EspecialidadesResponseDto
                {
                    idEspecialidad = e.idEspecialidad,
                    especialidad = e.especialidad,
                    descripcion = e.descripcion
                }).FirstOrDefaultAsync();

            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidad(int id, EspecialidadesFormDto especialidadDto)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null || especialidad.isDeleted)
            {
                return NotFound();
            }

            especialidad.especialidad = especialidadDto.especialidad;
            especialidad.descripcion = especialidadDto.descripcion;

            _context.Especialidades.Update(especialidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EspecialidadesResponseDto>> PostEspecialidad(EspecialidadesFormDto especialidadDto)
        {
            var especialidad = new Especialidad
            {
                idEspecialidad = 0,
                especialidad = especialidadDto.especialidad,
                descripcion = especialidadDto.descripcion
            };

            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspecialidad), new { id = especialidad.idEspecialidad }, especialidad);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null || especialidad.isDeleted)
            {
                return NotFound();
            }

            especialidad.isDeleted = true;
            _context.Especialidades.Update(especialidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecialidadExists(int id)
        {
            return _context.Especialidades.Any(e => e.idEspecialidad == id);
        }
    }
}
