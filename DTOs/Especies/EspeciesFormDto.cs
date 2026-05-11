using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Especies
{
    public class EspeciesFormDto
    {
        [Required(ErrorMessage = "La especie es requerida")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("especie")]
        public string especie { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
