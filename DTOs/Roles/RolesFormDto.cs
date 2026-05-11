using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Roles
{
    public class RolesFormDto
    {
        [Required(ErrorMessage = "Tipo de identificación es requerido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [JsonPropertyName("rolUsuario")]
        public string rolUsuario { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
