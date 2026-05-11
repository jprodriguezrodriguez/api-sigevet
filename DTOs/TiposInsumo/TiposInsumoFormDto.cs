using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.TiposInsumo
{
    public class TiposInsumoFormDto
    {
        [Required(ErrorMessage = "El tipo de insumo es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("tipoInsumo")]
        public string tipoInsumo { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
