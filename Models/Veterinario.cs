namespace sigevet.Models
{
    public class Veterinario
    {
        public int idPersonaVet {  get; set; }
        public required String numeroTarjetaProfesional { get; set; }
        public DateTime fechaRegistroVeterinario { get; set; } = DateTime.Now;
        public DateTime fechaActualizacionVeterinario { get; set; } = DateTime.Now;
        
        // Foráneas
        public required int idEstadoDisponibilidad { get; set; }

        // Relaciones
        public Persona persona { get; set; } = null!;
        public Estado estadoVeterinario { get; set; } = null!;
    }
}
