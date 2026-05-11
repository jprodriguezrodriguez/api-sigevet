namespace sigevet.DTOs.Estados
{
    public class EstadosResponseDto
    {
        public int idEstado { get; set; }
        public string estado { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public string? categoriaEstado { get; set; }
    }
}
