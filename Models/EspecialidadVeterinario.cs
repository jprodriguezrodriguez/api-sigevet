namespace sigevet.Models
{
    public class EspecialidadVeterinario:Auditable
    {
        public required int idVeterinarioEspecialidad {  get; set; }
        // Foráneas
        public int idVeterinario { get; set; }
        public int idEspecialidad { get; set; }

        // Relaciones
        public required Veterinario veterinarios {  get; set; }
        public required Especialidad especialidad { get; set; }
        
    }
}
