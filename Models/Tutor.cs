namespace sigevet.Models
{
    public class Tutor
    {
        public int idPersonaTut {  get; set; }
        public bool autorizaNotificaciones { get; set; }
        public DateTime fechaRegistroTutor { get; set; } = DateTime.Now;
        public DateTime fechaActualizacionTutor { get; set; } = DateTime.Now;

        // Foráneas
        public required int idEstadoCuentaTutor { get; set; }

        // Relaciones
        public Persona? persona { get; set; }
        public Estado? estadoCuenta { get; set; }
        public ICollection<TutorMascota> mascotasPorTutor { get; set; } = new List<TutorMascota>();
    }
}
