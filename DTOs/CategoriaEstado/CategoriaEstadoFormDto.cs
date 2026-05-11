using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.CategoriasEstado
{
    public class CategoriaEstadoFormDto
    {
        [Required(ErrorMessage = "La categoría de estado es requerida")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [JsonPropertyName("categoriaEstado")]
        public string categoriaEstado { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}