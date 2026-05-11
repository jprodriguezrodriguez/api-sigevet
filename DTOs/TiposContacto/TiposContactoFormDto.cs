using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.TiposContacto
{
    public class TiposContactoFormDto
    {
        [Required(ErrorMessage = "El tipo de contacto es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("tipoContacto")]
        public string tipoContacto { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
