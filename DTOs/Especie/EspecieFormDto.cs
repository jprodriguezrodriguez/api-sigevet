using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Especies
{
    public class EspecieFormDto
    {
        [Required(ErrorMessage = "La especie es requerida")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        [JsonPropertyName("especie")]
        public string especie { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}