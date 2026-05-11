namespace sigevet.DTOs.Brigadas
{
    public class BrigadaResponseDto
    {
        public int idBrigada { get; set; }
        public string nombreBrigada { get; set; } = string.Empty;
        public DateOnly fechaBrigada { get; set; }
        public TimeOnly horaInicio { get; set; }
        public TimeOnly horaFin { get; set; }
        public string ubicacion { get; set; } = string.Empty;
        public string? cobertura { get; set; }
        public string? observaciones { get; set; }

        public int idEstadoBrigada { get; set; }
    }
}