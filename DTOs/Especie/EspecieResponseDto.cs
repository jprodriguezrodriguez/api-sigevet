namespace sigevet.DTOs.Especies
{
    public class EspecieResponseDto
    {
        public int idEspecie { get; set; }
        public string especie { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}