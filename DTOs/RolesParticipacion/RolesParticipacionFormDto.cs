using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.RolesParticipacion
{
    public class RolesParticipacionFormDto
    {
        [Required(ErrorMessage = "El rol de participacion es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("rolParticipacion")]
        public string rolParticipacion { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
