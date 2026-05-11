namespace sigevet.DTOs.TiposAlerta
{
    public class TiposAlertaResponseDto
    {
        public int idTipoAlerta { get; set; }
        public string alerta { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}
