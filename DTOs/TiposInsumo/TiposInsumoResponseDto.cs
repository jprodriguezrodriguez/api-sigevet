namespace sigevet.DTOs.TiposInsumo
{
    public class TiposInsumoResponseDto
    {
        public int idTipoInsumo { get; set; }
        public string tipoInsumo { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}
