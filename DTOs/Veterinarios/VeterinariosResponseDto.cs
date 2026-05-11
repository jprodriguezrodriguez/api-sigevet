namespace sigevet.DTOs.Veterinarios
{
    public class VeterinariosResponseDto
    {
        public int idPersonaVet { get; set; }
        public string numeroTarjetaProfesional { get; set; } = string.Empty;
        public DateTime fechaRegistroVeterinario { get; set; }
        public DateTime fechaActualizacionVeterinario { get; set; }
        public string? persona { get; set; }
        public string? estadoVeterinario { get; set; }
    }
}
