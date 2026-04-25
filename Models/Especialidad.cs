namespace sigevet.Models
{
    public class Especialidad : Auditable
    {
        public required int idEspecialidad {  get; set; }
        public required string especialidad { get; set; }
        public string? descripcion { get; set; }
        // Relaciones
        public ICollection<EspecialidadVeterinario> veterinariosPorEspecialidad { get; set; } = new List<EspecialidadVeterinario>();

    }
}
