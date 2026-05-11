namespace sigevet.DTOs.InsumosSanitarios
{
    public class InsumosSanitariosResponseDto
    {
        public int idInsumoSanitario { get; set; }
        public string insumoSanitario { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public string? tipoInsumo { get; set; }
        public string? unidadMedida { get; set; }
        public string? estadoInsumo { get; set; }
    }
}
