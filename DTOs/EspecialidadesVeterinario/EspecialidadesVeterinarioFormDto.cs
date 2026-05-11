using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.EspecialidadesVeterinario
{
    public class EspecialidadesVeterinarioFormDto
    {
        [Required(ErrorMessage = "El veterinario es requerido")]
        [JsonPropertyName("idVeterinario")]
        public int idVeterinario { get; set; }

        [Required(ErrorMessage = "La especialidad es requerida")]
        [JsonPropertyName("idEspecialidad")]
        public int idEspecialidad { get; set; }
    }
}
