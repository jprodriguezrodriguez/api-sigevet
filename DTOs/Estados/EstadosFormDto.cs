using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Estados
{
    public class EstadosFormDto
    {
        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("estado")]
        public string estado { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }

        [Required(ErrorMessage = "La categoria de estado es requerida")]
        [JsonPropertyName("idCategoriaEstado")]
        public int idCategoriaEstado { get; set; }
    }
}
