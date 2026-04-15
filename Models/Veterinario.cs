namespace sigevet.Models
{
    public class Veterinario : Persona
    {
        public required int idVeterinario {  get; set; }
        public required String numeroTarjetaProfesional { get; set; }
        public required DateTime fechaRegistroVeterinario { get; set; } = DateTime.Now;
        public required DateTime fechaActualizacionVeterinario { get; set; } = DateTime.Now;
        
        // Foráneas
        public required int idEstadoDisponibilidad { get; set; }

        // Relaciones
        public required Estado estadoVeterinario { get; set; }
    }
}
