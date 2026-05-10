using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.TiposIdentificacion
{
    public class TipoIdentificacionFormDto
    {
        [Required(ErrorMessage = "Tipo de identificación es requerido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [JsonPropertyName("tipoIdentificacion")]
        public string tipoIdentificacion { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
