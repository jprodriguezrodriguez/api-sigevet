using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.TutoresMascota;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutoresMascotaController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TutoresMascotaController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutoresMascotaResponseDto>>> GetTutoresMascota()
        {
            var registros = await _context.TutoresMascota
                .Include(t => t.tutor)
                    .ThenInclude(t => t!.persona)
                .Include(t => t.mascota)
                .Where(t => !t.isDeleted)
                .Select(t => new TutoresMascotaResponseDto
                {
                    idTutorMascota = t.idTutorMascota,
                    tutor = t.tutor != null && t.tutor.persona != null ? t.tutor.persona.primerNombre + " " + t.tutor.persona.primerApellido : null,
                    mascota = t.mascota != null ? t.mascota.nombre : null
                }).ToListAsync();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutoresMascotaResponseDto>> GetTutorMascota(int id)
        {
            var registro = await _context.TutoresMascota
                .Include(t => t.tutor)
                    .ThenInclude(t => t!.persona)
                .Include(t => t.mascota)
                .Where(t => !t.isDeleted && t.idTutorMascota == id)
                .Select(t => new TutoresMascotaResponseDto
                {
                    idTutorMascota = t.idTutorMascota,
                    tutor = t.tutor != null && t.tutor.persona != null ? t.tutor.persona.primerNombre + " " + t.tutor.persona.primerApellido : null,
                    mascota = t.mascota != null ? t.mascota.nombre : null
                }).FirstOrDefaultAsync();

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorMascota(int id, TutoresMascotaFormDto registroDto)
        {
            var registro = await _context.TutoresMascota.FindAsync(id);
            if (registro == null || registro.isDeleted)
            {
                return NotFound();
            }

            registro.idPersonaTut = registroDto.idPersonaTut;
            registro.idMascota = registroDto.idMascota;

            _context.TutoresMascota.Update(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TutoresMascotaResponseDto>> PostTutorMascota(TutoresMascotaFormDto registroDto)
        {
            var registro = new TutorMascota
            {
                idPersonaTut = registroDto.idPersonaTut,
                idMascota = registroDto.idMascota
            };

            _context.TutoresMascota.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTutorMascota), new { id = registro.idTutorMascota }, registro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutorMascota(int id)
        {
            var registro = await _context.TutoresMascota.FindAsync(id);
            if (registro == null || registro.isDeleted)
            {
                return NotFound();
            }

            registro.isDeleted = true;
            _context.TutoresMascota.Update(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TutorMascotaExists(int id)
        {
            return _context.TutoresMascota.Any(e => e.idTutorMascota == id);
        }
    }
}
