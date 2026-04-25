namespace sigevet.Models
{
    public class Vacuna: Auditable
    {
        public int idVacuna {  get; set; }
        public required String nombre { get; set; }
        public required String numeroLote {get; set; }
        public DateTime fechaFabricacion { get; set; }
        public DateTime fechaVencimiento {  get; set; }

        // Foráneas
        public int idTipoVacuna { get; set; }
        public int idLaboratorio { get; set; }
        public int idEstadoVacuna { get; set; }

        // Relaciones
        public TipoVacuna tipoVacuna { get; set; } = null!;
        public Laboratorio laboratorio { get; set; } = null!;
        public Estado estadoVacuna { get; set; } = null!;
        public ICollection<Vacunacion> vacunaciones { get; set; } = new List<Vacunacion>();

    }
}
