using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.BrigadasVeterinarios
{
    public class BrigadaVeterinarioFormDto
    {
        [Required(ErrorMessage = "El veterinario es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un veterinario válido")]
        [JsonPropertyName("idVeterinario")]
        public int idVeterinario { get; set; }

        [Required(ErrorMessage = "La brigada es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una brigada válida")]
        [JsonPropertyName("idBrigada")]
        public int idBrigada { get; set; }

        [Required(ErrorMessage = "El rol de participación es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rol de participación válido")]
        [JsonPropertyName("idRolParticipacion")]
        public int idRolParticipacion { get; set; }
    }
}