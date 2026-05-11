namespace sigevet.DTOs.AlertasVacunacion
{
    public class AlertasVacunacionResponseDto
    {
        public int idAlerta { get; set; }
        public DateOnly fechaGeneracion { get; set; }
        public DateOnly fechaProgramada { get; set; }
        public string mensaje { get; set; } = string.Empty;
        public string? tipoAlerta { get; set; }
        public int idVacunacion { get; set; }
    }
}
