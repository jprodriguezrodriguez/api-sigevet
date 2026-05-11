using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Tutores
{
    public class TutoresFormDto
    {
        [Required(ErrorMessage = "La persona es requerida")]
        [JsonPropertyName("idPersonaTut")]
        public int idPersonaTut { get; set; }

        [JsonPropertyName("autorizaNotificaciones")]
        public bool autorizaNotificaciones { get; set; }

        [Required(ErrorMessage = "El estado del tutor es requerido")]
        [JsonPropertyName("idEstadoTutor")]
        public int idEstadoTutor { get; set; }
    }
}
