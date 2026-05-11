namespace sigevet.DTOs.TiposMovimiento
{
    public class TiposMovimientoResponseDto
    {
        public int idTipoMovimiento { get; set; }
        public string tipoMovimiento { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}
