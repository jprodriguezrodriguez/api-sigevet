namespace sigevet.Models
{
    public class TipoInsumo : Auditable
    {
        public int idTipoInsumo { get; set; }
        public required String tipoInsumo { get; set; }
        public String? descripcion { get; set; }
    }
}
