using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Brigadas
{
    public class BrigadaFormDto
    {
        [Required(ErrorMessage = "El nombre de la brigada es requerido")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        [JsonPropertyName("nombreBrigada")]
        public string nombreBrigada { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de la brigada es requerida")]
        [JsonPropertyName("fechaBrigada")]
        public DateOnly fechaBrigada { get; set; }

        [Required(ErrorMessage = "La hora de inicio es requerida")]
        [JsonPropertyName("horaInicio")]
        public TimeOnly horaInicio { get; set; }

        [Required(ErrorMessage = "La hora de finalización es requerida")]
        [JsonPropertyName("horaFin")]
        public TimeOnly horaFin { get; set; }

        [Required(ErrorMessage = "La ubicación es requerida")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [JsonPropertyName("ubicacion")]
        public string ubicacion { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("cobertura")]
        public string? cobertura { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [JsonPropertyName("observaciones")]
        public string? observaciones { get; set; }

        [Required(ErrorMessage = "El estado de la brigada es requerido")]
        [JsonPropertyName("idEstadoBrigada")]
        public int idEstadoBrigada { get; set; }
    }
}