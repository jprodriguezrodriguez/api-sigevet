namespace sigevet.Models
{
    public class Raza: Auditable
    {
        public required int idRaza { get; set; }
        public required string raza { get; set; }
        public string? descripcion { get; set; }
        // Foráneas
        public int idEspecie { get; set; }
        // Relaciones
        public Especie especie { get; set; } = null!;
    }
}
