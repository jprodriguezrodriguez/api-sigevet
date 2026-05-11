using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Especialidades
{
    public class EspecialidadFormDto
    {
        [Required(ErrorMessage = "La especialidad es requerida")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        [JsonPropertyName("especialidad")]
        public string especialidad { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}