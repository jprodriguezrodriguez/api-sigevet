using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.AlertasVacunacion
{
    public class AlertasVacunacionFormDto
    {
        [Required(ErrorMessage = "La fecha de generacion es requerida")]
        [JsonPropertyName("fechaGeneracion")]
        public DateOnly fechaGeneracion { get; set; }

        [Required(ErrorMessage = "La fecha programada es requerida")]
        [JsonPropertyName("fechaProgramada")]
        public DateOnly fechaProgramada { get; set; }

        [Required(ErrorMessage = "El mensaje es requerido")]
        [StringLength(255, ErrorMessage = "Maximo 255 caracteres")]
        [JsonPropertyName("mensaje")]
        public string mensaje { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de alerta es requerido")]
        [JsonPropertyName("idTipoAlerta")]
        public int idTipoAlerta { get; set; }

        [Required(ErrorMessage = "La vacunacion es requerida")]
        [JsonPropertyName("idVacunacion")]
        public int idVacunacion { get; set; }
    }
}
