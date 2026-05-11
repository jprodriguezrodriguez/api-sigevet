namespace sigevet.DTOs.Auth
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public required string Usuario { get; set; }
        public required string Rol { get; set; }
    }
}