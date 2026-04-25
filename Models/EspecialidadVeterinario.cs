namespace sigevet.Models
{
    public class EspecialidadVeterinario:Auditable
    {
        public int idVeterinarioEspecialidad {  get; set; }
        // Foráneas
        public int idVeterinario { get; set; }
        public int idEspecialidad { get; set; }

        // Relaciones
        public Veterinario veterinarios {  get; set; } = null!;
        public Especialidad especialidad { get; set; } = null!;

    }
}
