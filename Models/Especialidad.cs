namespace sigevet.Models
{
    public class Especialidad : Auditable
    {
        public int idEspecialidad { get; set; }

        public string especialidad { get; set; } = string.Empty;

        public string? descripcion { get; set; }
        // Relaciones
        public ICollection<EspecialidadVeterinario> veterinariosPorEspecialidad { get; set; } = new List<EspecialidadVeterinario>();

    }
}
