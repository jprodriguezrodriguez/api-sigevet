using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sigevet.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace sigevet.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SigevetDbContext _context;

        public TokenService(IConfiguration configuration, SigevetDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateJwtToken(CuentasUsuarios user)
        {
            var jwtKey = _configuration["Jwt:Key"]!;
            var jwtIssuer = _configuration["Jwt:Issuer"]!;
            var jwtAudience = _configuration["Jwt:Audience"]!;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("idCuentaUsuario", user.idCuentaUsuario.ToString()),
                new Claim("idRol", user.idRol.ToString()),
                new Claim("idPersona", user.idPersona.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15), // Token expira en 15 minutos
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task SaveRefreshTokenAsync(CuentasUsuarios user, string refreshToken)
        {
            var refreshTokenHash = HashToken(refreshToken);

            var refreshTokenEntity = new RefrescarToken
            {
                tokenHash = refreshTokenHash,
                fechaCreacion = DateTime.UtcNow,
                fechaExpiracion = DateTime.UtcNow.AddDays(7), // Refresh token expira en 7 días
                idCuentaUsuario = user.idCuentaUsuario
            };

            _context.RefrescarTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<CuentasUsuarios?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var refreshTokenHash = HashToken(refreshToken);

            var refreshTokenEntity = await _context.RefrescarTokens
                .Include(rt => rt.tokensPorCuentaUsuario)
                .ThenInclude(u => u.rolesUsuario)
                .FirstOrDefaultAsync(rt => rt.tokenHash == refreshTokenHash && rt.estaActivo());

            return refreshTokenEntity?.tokensPorCuentaUsuario;
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var refreshTokenHash = HashToken(refreshToken);

            var refreshTokenEntity = await _context.RefrescarTokens
                .FirstOrDefaultAsync(rt => rt.tokenHash == refreshTokenHash);

            if (refreshTokenEntity != null)
            {
                refreshTokenEntity.fechaRevocacion = DateTime.UtcNow;
                _context.RefrescarTokens.Update(refreshTokenEntity);
                await _context.SaveChangesAsync();
            }
        }

        private string HashToken(string token)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}