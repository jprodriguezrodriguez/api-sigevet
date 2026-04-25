namespace sigevet.Models
{
    public class Especie : Auditable
    {
        public required int idEspecie { get; set; }
        public required string especie { get; set; }
        public string? descripcion { get; set; }

        // Relaciones
        public ICollection<Raza> razasPorEspecie { get; set; } = new List<Raza>();
    }

}
