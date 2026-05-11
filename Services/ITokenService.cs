using sigevet.Models;

namespace sigevet.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(CuentasUsuarios user);
        string GenerateRefreshToken();
        Task SaveRefreshTokenAsync(CuentasUsuarios user, string refreshToken);
        Task<CuentasUsuarios?> GetUserByRefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}