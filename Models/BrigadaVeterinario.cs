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
        public Veterinario? veterinario { get; set; }
        public Brigada? brigadas { get; set; }
        public RolParticipacion? rolParticipacion { get; set;}
    }
}
