namespace sigevet.DTOs.Laboratorios
{
    public class LaboratoriosResponseDto
    {
        public int idLaboratorio { get; set; }
        public string laboratorio { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public string? direccion { get; set; }
    }
}
