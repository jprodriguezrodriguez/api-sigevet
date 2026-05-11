namespace sigevet.DTOs.Especialidades
{
    public class EspecialidadResponseDto
    {
        public int idEspecialidad { get; set; }
        public string especialidad { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}