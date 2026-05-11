using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Brigadas
{
    public class BrigadasFormDto
    {
        [Required(ErrorMessage = "El nombre de la brigada es requerido")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("nombreBrigada")]
        public string nombreBrigada { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de brigada es requerida")]
        [JsonPropertyName("fechaBrigada")]
        public DateOnly fechaBrigada { get; set; }

        [Required(ErrorMessage = "La hora de inicio es requerida")]
        [JsonPropertyName("horaInicio")]
        public TimeOnly horaInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es requerida")]
        [JsonPropertyName("horaFin")]
        public TimeOnly horaFin { get; set; }

        [Required(ErrorMessage = "La ubicacion es requerida")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("ubicacion")]
        public string ubicacion { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("cobertura")]
        public string? cobertura { get; set; }

        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("observaciones")]
        public string? observaciones { get; set; }

        [Required(ErrorMessage = "El estado de la brigada es requerido")]
        [JsonPropertyName("idEstadoBrigada")]
        public int idEstadoBrigada { get; set; }
    }
}
