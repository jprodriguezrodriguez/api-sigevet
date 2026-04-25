namespace sigevet.Models
{
    public class TutorMascota : Auditable
    {
        public int idTutorMascota { get; set; }

        // Foráneas
        public int idPersonaTut { get; set; }
        public int idMascota { get; set; }

        // Relaciones
        public Tutor tutor { get; set; } = null!;
        public Mascota mascota { get; set; } = null!;
    }
}
