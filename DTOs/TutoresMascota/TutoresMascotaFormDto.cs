using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.TutoresMascota
{
    public class TutoresMascotaFormDto
    {
        [Required(ErrorMessage = "El tutor es requerido")]
        [JsonPropertyName("idPersonaTut")]
        public int idPersonaTut { get; set; }

        [Required(ErrorMessage = "La mascota es requerida")]
        [JsonPropertyName("idMascota")]
        public int idMascota { get; set; }
    }
}
