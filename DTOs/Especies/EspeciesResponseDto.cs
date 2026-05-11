namespace sigevet.DTOs.Especies
{
    public class EspeciesResponseDto
    {
        public int idEspecie { get; set; }
        public string especie { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}
