using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Tutores;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutoresController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public TutoresController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutoresResponseDto>>> GetTutores()
        {
            var tutores = await _context.Tutores
                .Include(t => t.persona)
                .Include(t => t.estadoTutor)
                .Where(t => t.persona == null || !t.persona.isDeleted)
                .Select(t => new TutoresResponseDto
                {
                    idPersonaTut = t.idPersonaTut,
                    autorizaNotificaciones = t.autorizaNotificaciones,
                    fechaRegistroTutor = t.fechaRegistroTutor,
                    fechaActualizacionTutor = t.fechaActualizacionTutor,
                    persona = t.persona != null ? t.persona.primerNombre + " " + t.persona.primerApellido : null,
                    estadoTutor = t.estadoTutor != null ? t.estadoTutor.estado : null
                }).ToListAsync();

            return Ok(tutores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutoresResponseDto>> GetTutor(int id)
        {
            var tutor = await _context.Tutores
                .Include(t => t.persona)
                .Include(t => t.estadoTutor)
                .Where(t => t.idPersonaTut == id && (t.persona == null || !t.persona.isDeleted))
                .Select(t => new TutoresResponseDto
                {
                    idPersonaTut = t.idPersonaTut,
                    autorizaNotificaciones = t.autorizaNotificaciones,
                    fechaRegistroTutor = t.fechaRegistroTutor,
                    fechaActualizacionTutor = t.fechaActualizacionTutor,
                    persona = t.persona != null ? t.persona.primerNombre + " " + t.persona.primerApellido : null,
                    estadoTutor = t.estadoTutor != null ? t.estadoTutor.estado : null
                }).FirstOrDefaultAsync();

            if (tutor == null)
            {
                return NotFound();
            }

            return Ok(tutor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutor(int id, TutoresFormDto tutorDto)
        {
            if (id != tutorDto.idPersonaTut)
            {
                return BadRequest();
            }

            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }

            tutor.autorizaNotificaciones = tutorDto.autorizaNotificaciones;
            tutor.idEstadoTutor = tutorDto.idEstadoTutor;
            tutor.fechaActualizacionTutor = DateTime.Now;

            _context.Tutores.Update(tutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TutoresResponseDto>> PostTutor(TutoresFormDto tutorDto)
        {
            var tutor = new Tutor
            {
                idPersonaTut = tutorDto.idPersonaTut,
                autorizaNotificaciones = tutorDto.autorizaNotificaciones,
                idEstadoTutor = tutorDto.idEstadoTutor
            };

            _context.Tutores.Add(tutor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TutorExists(tutor.idPersonaTut))
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction(nameof(GetTutor), new { id = tutor.idPersonaTut }, tutor);
        }

        private bool TutorExists(int id)
        {
            return _context.Tutores.Any(e => e.idPersonaTut == id);
        }
    }
}
