using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.EspecialidadesVeterinarios
{
    public class EspecialidadVeterinarioFormDto
    {
        [Required(ErrorMessage = "El veterinario es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un veterinario válido")]
        [JsonPropertyName("idVeterinario")]
        public int idVeterinario { get; set; }

        [Required(ErrorMessage = "La especialidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad válida")]
        [JsonPropertyName("idEspecialidad")]
        public int idEspecialidad { get; set; }
    }
}