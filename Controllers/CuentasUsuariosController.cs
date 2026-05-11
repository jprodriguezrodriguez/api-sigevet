using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sigevet.DTOs.Auth;
using sigevet.DTOs.CuentasUsuarios;
using sigevet.Models;
using sigevet.Services;
using System.Security.Cryptography;

namespace sigevet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasUsuariosController : ControllerBase
    {
        private readonly SigevetDbContext _context;
        private readonly ITokenService _tokenService;

        public CuentasUsuariosController(SigevetDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/CuentasUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentasUsuariosResponseDto>>> GetCuentasUsuarios()
        {
            var cuentas = await _context.CuentasUsuarios
                .Where(c => !c.isDeleted)
                .Select(rol => new CuentasUsuariosResponseDto
                {
                    idCuentaUsuario = rol.idCuentaUsuario,
                    usuario = rol.usuario,
                    ultimoInicioSesion = rol.ultimoInicioSesion,
                    nombreRol = rol.rolesUsuario != null ? rol.rolesUsuario.rolUsuario : "",
                    EstadoCuenta = rol.estadoCuenta != null ? rol.estadoCuenta.estado : "",
                    nombrePersona = rol.persona != null ? (rol.persona.primerNombre + " " + rol.persona.primerApellido) : ""
                })
                .ToListAsync();

            return Ok(cuentas);
        }

        // GET: api/CuentasUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentasUsuariosResponseDto>> GetCuentasUsuarios(int id)
        {
            var cuenta = await _context.CuentasUsuarios
                .Where(c => !c.isDeleted && c.idCuentaUsuario == id)
                .Select(rol => new CuentasUsuariosResponseDto
                {
                    idCuentaUsuario = rol.idCuentaUsuario,
                    usuario = rol.usuario,
                    ultimoInicioSesion = rol.ultimoInicioSesion,
                    nombreRol = rol.rolesUsuario != null ? rol.rolesUsuario.rolUsuario : "",
                    EstadoCuenta = rol.estadoCuenta != null ? rol.estadoCuenta.estado : "",
                    nombrePersona = rol.persona != null ? (rol.persona.primerNombre + " " + rol.persona.primerApellido) : ""
                })
                .ToListAsync();

            if (cuenta == null)
            {
                return NotFound();
            }

            return Ok(cuenta);
        }

        // PUT: api/CuentasUsuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentasUsuarios(int id, CuentasUsuariosFormDto cuentaUsuario)
        {
            var cuentaExistente = await _context.CuentasUsuarios.FindAsync(id);
            if (cuentaExistente == null || cuentaExistente.isDeleted)
            {
                return NotFound();
            }

            if (!PersonaExist(cuentaUsuario.idPersona) || !RolExist(cuentaUsuario.idRol))
            {
                return NotFound();
            }

            cuentaUsuario.usuario = cuentaUsuario.usuario.Trim();

            // Verificar si otro usuario (no eliminado) tiene el mismo nombre
            var existeOtraCuentaUsuario = await _context.CuentasUsuarios
                .AnyAsync(
                    cu => cu.idCuentaUsuario != id &&
                    cu.usuario.ToLower() == cuentaUsuario.usuario.ToLower() &&
                    !cu.isDeleted);

            if (existeOtraCuentaUsuario)
            {
                return BadRequest(new
                {
                    mensaje = "Ya existe cuenta con el mismo nombre. (Nombre ingresado: " + cuentaUsuario.usuario + ")"
                });
            }

            cuentaExistente.idRol = cuentaUsuario.idRol;
            cuentaExistente.idPersona = cuentaUsuario.idPersona;
            cuentaExistente.usuario = cuentaUsuario.usuario;

            var (hashNuevaContrasenia, saltNueva) = ObtenerContraseniaHash(cuentaUsuario.constrasenia);
            if (cuentaExistente.contraseniaHash != hashNuevaContrasenia)
            {
                cuentaExistente.intentosFallidos = 0;
                cuentaExistente.contraseniaHash = hashNuevaContrasenia;
                cuentaExistente.salt = saltNueva;
                cuentaExistente.fechaDesbloqueo = null;
            }

            _context.CuentasUsuarios.Update(cuentaExistente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/CuentasUsuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuentasUsuarios>> PostCuentasUsuarios(CuentasUsuariosFormDto cuentaUsuario)
        {
            if (!PersonaExist(cuentaUsuario.idPersona) || !RolExist(cuentaUsuario.idRol))
            {
                return NotFound();
            }

            cuentaUsuario.usuario = cuentaUsuario.usuario.Trim();

            // Buscar si existe una cuenta con ese usuario (eliminada o no)
            var cuentaExistente = await _context.CuentasUsuarios.FirstOrDefaultAsync(c => c.usuario.ToLower() == cuentaUsuario.usuario.ToLower());

            if (cuentaExistente != null)
            {
                // Si existe y está activa, error
                if (!cuentaExistente.isDeleted)
                {
                    return BadRequest(new
                    {
                        mensaje = "Ya existe cuenta con el mismo nombre. (Nombre ingresado: " + cuentaUsuario.usuario + ")"
                    });
                }

                // Si existe pero está eliminada, reactivarla
                var (hashNuevaContrasenia, saltNueva) = ObtenerContraseniaHash(cuentaUsuario.constrasenia);
                cuentaExistente.idRol = cuentaUsuario.idRol;
                cuentaExistente.idPersona = cuentaUsuario.idPersona;
                cuentaExistente.contraseniaHash = hashNuevaContrasenia;
                cuentaExistente.salt = saltNueva;
                cuentaExistente.intentosFallidos = 0;
                cuentaExistente.fechaDesbloqueo = null;
                cuentaExistente.isDeleted = false;
                _context.CuentasUsuarios.Update(cuentaExistente);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCuentasUsuarios", new { id = cuentaExistente.idCuentaUsuario }, cuentaExistente);
            }

            // Si no existe, crear nueva
            var (hashed, salt) = ObtenerContraseniaHash(cuentaUsuario.constrasenia);

            var cuentaUsuarioNuevo = new CuentasUsuarios
            {
                usuario = cuentaUsuario.usuario,
                contraseniaHash = hashed,
                salt = salt,
                ultimoInicioSesion = null,
                intentosFallidos = 0,
                fechaDesbloqueo = null,
                idEstadoCuenta = 0,
                idRol = cuentaUsuario.idRol,
                idPersona = cuentaUsuario.idPersona
            };

            _context.CuentasUsuarios.Add(cuentaUsuarioNuevo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentasUsuarios", new { id = cuentaUsuarioNuevo.idCuentaUsuario }, cuentaUsuarioNuevo);
        }

        // POST: api/CuentasUsuarios/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            var user = await _context.CuentasUsuarios
                .Include(u => u.rolesUsuario)
                .Include(u => u.estadoCuenta)
                .FirstOrDefaultAsync(u => u.usuario.ToLower() == loginRequest.Usuario.ToLower() && !u.isDeleted);

            if (user == null)
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }

            // Verificar si la cuenta está bloqueada
            if (user.fechaDesbloqueo.HasValue && user.fechaDesbloqueo > DateTime.Now)
            {
                return Unauthorized(new { message = "Cuenta bloqueada temporalmente" });
            }

            // Verificar contraseña
            if (!VerificarContrasenia(loginRequest.Contrasenia, user.contraseniaHash, user.salt))
            {
                // Incrementar intentos fallidos
                user.intentosFallidos++;
                if (user.intentosFallidos >= 5)
                {
                    user.fechaDesbloqueo = DateTime.Now.AddMinutes(30); // Bloquear por 30 minutos
                }
                _context.CuentasUsuarios.Update(user);
                await _context.SaveChangesAsync();

                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }

            // Resetear intentos fallidos y actualizar último inicio de sesión
            user.intentosFallidos = 0;
            user.fechaDesbloqueo = null;
            user.ultimoInicioSesion = DateTime.Now;
            _context.CuentasUsuarios.Update(user);
            await _context.SaveChangesAsync();

            // Generar tokens
            var token = _tokenService.GenerateJwtToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, refreshToken);

            var response = new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.Now.AddMinutes(15),
                Usuario = user.usuario,
                Rol = user.rolesUsuario?.rolUsuario ?? ""
            };

            return Ok(response);
        }

        // POST: api/CuentasUsuarios/refresh
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken(RefreshTokenRequestDto refreshRequest)
        {
            var user = await _tokenService.GetUserByRefreshTokenAsync(refreshRequest.RefreshToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Refresh token inválido" });
            }

            // Revocar el refresh token usado
            await _tokenService.RevokeRefreshTokenAsync(refreshRequest.RefreshToken);

            // Generar nuevos tokens
            var newToken = _tokenService.GenerateJwtToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user, newRefreshToken);

            var response = new LoginResponseDto
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.Now.AddMinutes(15),
                Usuario = user.usuario,
                Rol = user.rolesUsuario?.rolUsuario ?? ""
            };

            return Ok(response);
        }

        // DELETE: api/CuentasUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentasUsuarios(int id)
        {
            var cuentasUsuarios = await _context.CuentasUsuarios.FindAsync(id);
            if (cuentasUsuarios == null)
            {
                return NotFound();
            }

            cuentasUsuarios.isDeleted = true;

            _context.CuentasUsuarios.Update(cuentasUsuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExist(int id)
        {
            return _context.Personas.Any(e => e.idPersona == id && !e.isDeleted);
        }

        private bool RolExist(int id)
        {
            return _context.Roles.Any(e => e.idRol == id && !e.isDeleted);
        }

        private (string hash, string salt) ObtenerContraseniaHash(string contrasenia)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
            string salt = Convert.ToBase64String(saltBytes);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: contrasenia,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
            );

            return (hashed, salt);
        }

        private bool VerificarContrasenia(string contrasenia, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: contrasenia,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
            );

            return hashed == hash;
        }
    }
}
