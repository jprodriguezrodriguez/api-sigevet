namespace sigevet.Models
{
    public class TipoMovimiento : Auditable
    {
        public required int idTipoMovimiento { get; set; }
        public required string tipoMovimiento { get; set; }
        public string? descripcion { get; set; }
    }
}
