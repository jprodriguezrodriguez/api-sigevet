namespace sigevet.Models
{
    public class Estado : Auditable
    {
        public int idEstado {  get; set; }
        public required String estado { get; set; }
        public String? descripcion { get; set; }

        // Llave foránea
        public int idCategoriaEstado {  get; set; }

        // Relación con Categoría
        public CategoriaEstado? categoriaEstado { get; set; }

        public void registrarEstado(String nombreEstado) { }

        public void actualizarEstado(int idEstado) { }
    }
}
