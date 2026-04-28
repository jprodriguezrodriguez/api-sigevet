namespace sigevet.Models
{
    public class Mascota: Auditable
    {
        public int idMascota {  get; set; }
        public String? nombre {  get; set; }
        public DateTime fechaNacimiento { get; set; }
        public char sexo { get; set; }
        public String? color { get; set; }
        public Decimal peso { get; set; }
        public String? seniasParticulares { get; set; }

        // Foráneas
        public int idRaza { get; set; }
        public int idEstadoMascota { get; set; }

        // Relaciones
        public Raza? raza { get; set; }
        public Estado? estadoMascota { get; set; }

        public ICollection<TutorMascota> tutoresMascota { get; set; } = new List<TutorMascota>();
        public ICollection<Vacunacion> vacunacionesMascota { get; set; } = new List<Vacunacion>();
    }
}
