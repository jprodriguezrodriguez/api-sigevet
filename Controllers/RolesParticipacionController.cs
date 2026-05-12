using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.RolesParticipacion;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesParticipacionController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public RolesParticipacionController(SigevetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesParticipacionResponseDto>>> GetRolesParticipacion()
        {
            var roles = await _context.RolesParticipacion
                .Where(r => !r.isDeleted)
                .Select(r => new RolesParticipacionResponseDto
                {
                    idRolParticipacion = r.idRolParticipacion,
                    rolParticipacion = r.rolParticipacion,
                    descripcion = r.descripcion
                }).ToListAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolesParticipacionResponseDto>> GetRolParticipacion(int id)
        {
            var rol = await _context.RolesParticipacion
                .Where(r => !r.isDeleted && r.idRolParticipacion == id)
                .Select(r => new RolesParticipacionResponseDto
                {
                    idRolParticipacion = r.idRolParticipacion,
                    rolParticipacion = r.rolParticipacion,
                    descripcion = r.descripcion
                }).FirstOrDefaultAsync();

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolParticipacion(int id, [FromForm] RolesParticipacionFormDto rolDto)
        {
            var rol = await _context.RolesParticipacion.FindAsync(id);
            if (rol == null || rol.isDeleted)
            {
                return NotFound();
            }

            rol.rolParticipacion = rolDto.rolParticipacion;
            rol.descripcion = rolDto.descripcion;
            _context.RolesParticipacion.Update(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<RolesParticipacionResponseDto>> PostRolParticipacion([FromForm] RolesParticipacionFormDto rolDto)
        {
            var rol = new RolParticipacion
            {
                rolParticipacion = rolDto.rolParticipacion,
                descripcion = rolDto.descripcion
            };

            _context.RolesParticipacion.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRolParticipacion), new { id = rol.idRolParticipacion }, rol);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolParticipacion(int id)
        {
            var rolParticipacion = await _context.RolesParticipacion.FindAsync(id);
            if (rolParticipacion == null || rolParticipacion.isDeleted)
            {
                return NotFound();
            }

            rolParticipacion.isDeleted = true;
            _context.RolesParticipacion.Update(rolParticipacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolParticipacionExists(int id)
        {
            return _context.RolesParticipacion.Any(e => e.idRolParticipacion == id);
        }
    }
}
