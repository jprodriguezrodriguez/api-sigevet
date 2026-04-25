namespace sigevet.Models
{
    public class BrigadaVeterinario : Auditable
    {
        public int idBrigadaVeterinario { get; set; }
        
        // Foráneas
        public int idVeterinario { get; set; }
        public int idBrigada {  get; set; }
        public int idRolParticipacion { get; set; }

        // Relaciones
        public Veterinario veterinario { get; set; } = null!;
        public Brigada brigadas { get; set; } = null!;
        public RolParticipacion rolParticipacion { get; set;} = null!;
    }
}
