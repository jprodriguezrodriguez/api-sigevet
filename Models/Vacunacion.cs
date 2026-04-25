namespace sigevet.Models
{
    public class Vacunacion : Auditable
    {
        public int idVacunacion {  get; set; }
        public DateTime fechaAplicacion { get; set; }
        public Decimal dosisAplicada { get; set; }
        public int numeroDosis { get; set; }
        public String? observaciones { get; set; }
        public DateOnly? proximaFecha {  get; set; }

        // Foráneas
        public int idVacuna { get; set; }
        public int idMascota { get; set; }

        // Relaciones
        public Vacuna vacuna { get; set; } = null!;
        public Mascota mascota { get; set; } = null!;
        public ICollection<AlertaVacunacion> alertasVacunacion = new List<AlertaVacunacion>();
    }
}
