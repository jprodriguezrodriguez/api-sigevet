using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Veterinarios
{
    public class VeterinariosFormDto
    {
        [Required(ErrorMessage = "La persona es requerida")]
        [JsonPropertyName("idPersonaVet")]
        public int idPersonaVet { get; set; }

        [Required(ErrorMessage = "La tarjeta profesional es requerida")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("numeroTarjetaProfesional")]
        public string numeroTarjetaProfesional { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado de disponibilidad es requerido")]
        [JsonPropertyName("idEstadoDisponibilidad")]
        public int idEstadoDisponibilidad { get; set; }
    }
}
