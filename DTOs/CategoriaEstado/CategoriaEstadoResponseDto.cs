namespace sigevet.DTOs.CategoriasEstado
{
    public class CategoriaEstadoResponseDto
    {
        public int idCategoriaEstado { get; set; }
        public string categoriaEstado { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}