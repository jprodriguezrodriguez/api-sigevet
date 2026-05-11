using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.BrigadasVeterinarios
{
    public class BrigadasVeterinariosFormDto
    {
        [Required(ErrorMessage = "El veterinario es requerido")]
        [JsonPropertyName("idVeterinario")]
        public int idVeterinario { get; set; }

        [Required(ErrorMessage = "La brigada es requerida")]
        [JsonPropertyName("idBrigada")]
        public int idBrigada { get; set; }

        [Required(ErrorMessage = "El rol de participacion es requerido")]
        [JsonPropertyName("idRolParticipacion")]
        public int idRolParticipacion { get; set; }
    }
}
