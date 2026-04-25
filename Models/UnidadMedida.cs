namespace sigevet.Models
{
    public class UnidadMedida : Auditable
    {
        public int idUnidadMedida { get; set; }
        public required string unidadMedida { get; set; }
        public string? descripcion { get; set; }
    }
}
