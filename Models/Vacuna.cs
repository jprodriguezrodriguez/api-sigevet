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
        public TipoVacuna? tipoVacuna { get; set; }
        public Laboratorio? laboratorio { get; set; }
        public Estado? estadoVacuna { get; set; }
        public ICollection<Vacunacion> vacunaciones { get; set; } = new List<Vacunacion>();

    }
}
