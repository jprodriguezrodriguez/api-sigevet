namespace sigevet.DTOs.Auth
{
    public class LoginRequestDto
    {
        public required string Usuario { get; set; }
        public required string Contrasenia { get; set; }
    }
}