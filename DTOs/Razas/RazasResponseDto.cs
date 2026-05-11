namespace sigevet.DTOs.Razas
{
    public class RazasResponseDto
    {
        public int idRaza { get; set; }
        public string raza { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public string? especie { get; set; }
    }
}
