using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Roles;
using sigevet.Models;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly SigevetDbContext _context;

        public RolesController(SigevetDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesResponseDto>>> GetRoles()
        {
            var roles = await _context.Roles
                .Where(rol => !rol.isDeleted)
                .Select(rol => new RolesResponseDto
                {
                    idRol = rol.idRol,
                    rolUsuario = rol.rolUsuario,
                    descripcion = rol.descripcion
                }).ToListAsync();
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolesResponseDto>> GetRoles(int id)
        {
            var rol = await _context.Roles
                .Where(rol => !rol.isDeleted)
                .Where(rol => rol.idRol == id)
                .Select(rol => new RolesResponseDto
                {
                    idRol = rol.idRol,
                    rolUsuario = rol.rolUsuario,
                    descripcion = rol.descripcion
                }).FirstOrDefaultAsync();

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, RolesFormDto rolDto)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            // Verificar si otro rol (no eliminado) tiene el mismo nombre
            var rolDuplicado = await _context.Roles.FirstOrDefaultAsync(e =>
                e.rolUsuario == rolDto.rolUsuario &&
                e.idRol != id &&
                !e.isDeleted
            );

            if (rolDuplicado != null)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe un rol con el mismo nombre. (Nombre ingresado: " + rolDto.rolUsuario + ")"
                });
            }

            rol.rolUsuario = rolDto.rolUsuario;
            rol.descripcion = rolDto.descripcion;

            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostRoles(RolesFormDto rolDto)
        {
            // Buscar si existe un rol con ese nombre (eliminado o no)
            var rolExistente = await _context.Roles.FirstOrDefaultAsync(e => e.rolUsuario == rolDto.rolUsuario);

            if (rolExistente != null)
            {
                // Si existe y está activo, error
                if (!rolExistente.isDeleted)
                {
                    return BadRequest(new
                    {
                        mensaje = "Ya existe un rol con el mismo nombre. (Nombre ingresado: " + rolDto.rolUsuario + ")"
                    });
                }

                // Si existe pero está eliminado, reactivarlo
                rolExistente.descripcion = rolDto.descripcion;
                rolExistente.isDeleted = false;
                _context.Roles.Update(rolExistente);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRoles", new { id = rolExistente.idRol }, rolExistente);
            }

            // Si no existe, crear nuevo
            var rol = new Roles
            {
                rolUsuario = rolDto.rolUsuario,
                descripcion = rolDto.descripcion
            };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoles", new { id = rol.idRol }, rol);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            var existeEnCuentasDeUsuario = await _context.CuentasUsuarios.FirstOrDefaultAsync(e => e.idRol == id && !e.isDeleted);
            if (existeEnCuentasDeUsuario != null)
            {
                return BadRequest(new
                {
                    mensaje = "El Rol esta cruzado con otros datos, no se puede eliminar"
                });
            }

            roles.isDeleted = true;
            _context.Roles.Update(roles);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
