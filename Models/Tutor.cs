namespace sigevet.Models
{
    public class Tutor
    {
        public int idPersonaTut {  get; set; }
        public bool autorizaNotificaciones { get; set; }
        public bool isDeleted { get; set; }
        public DateTime fechaRegistroTutor { get; set; } = DateTime.Now;
        public DateTime fechaActualizacionTutor { get; set; } = DateTime.Now;

        // Foráneas
        public required int idEstadoTutor { get; set; }

        // Relaciones
        public Persona? persona { get; set; }
        public Estado? estadoTutor { get; set; }
        public ICollection<TutorMascota> mascotasPorTutor { get; set; } = new List<TutorMascota>();
    }
}
