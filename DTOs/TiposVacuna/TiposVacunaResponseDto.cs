namespace sigevet.DTOs.TiposVacuna
{
    public class TiposVacunaResponseDto
    {
        public int idTipoVacuna { get; set; }
        public string tipoVacuna { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}
