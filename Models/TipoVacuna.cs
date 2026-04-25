namespace sigevet.Models
{
    public class TipoVacuna : Auditable
    {
        public required int idTipoVacuna { get; set; }
        public required string tipoVacuna { get; set; }
        public string? descripcion { get; set; }

        // Relaciones
        public ICollection<EsquemaVacunacion> esquemasVacunacion { get; set; } = new List<EsquemaVacunacion>();
    }
}
