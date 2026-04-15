namespace sigevet.Models
{
    public class CategoriaEstado: Auditable
    {
        public required int idCategoriaEstado {  get; set; }
        public required string categoriaEstado { get; set; }
        public string? descripcion { get; set; }

        public ICollection<Estado>? estadosPorCategoria { get; set; }  
    }
}
