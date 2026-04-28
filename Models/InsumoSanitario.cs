namespace sigevet.Models
{
    public class InsumoSanitario: Auditable
    {
        public int idInsumoSanitario {  get; set; }
        public required String insumoSanitario { get; set; }
        public String? descripcion { get; set; }

        // Foráneas
        public int idTipoInsumo {  get; set; }
        public int idUnidadMedida { get; set; }
        public int idEstadoInsumo { get; set; }

        // Relaciones
        public TipoInsumo? tipoInsumo { get; set; } 
        public UnidadMedida? unidadMedida { get; set; }
        public Estado? estadoInsumo { get; set; }

        public ICollection<Inventario> inventarios { get; set; } = new List<Inventario>();
    }
}