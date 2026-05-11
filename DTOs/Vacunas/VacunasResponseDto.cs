namespace sigevet.DTOs.Vacunas
{
    public class VacunasResponseDto
    {
        public int idVacuna { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string numeroLote { get; set; } = string.Empty;
        public DateTime fechaFabricacion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string? tipoVacuna { get; set; }
        public string? laboratorio { get; set; }
        public string? estadoVacuna { get; set; }
    }
}
