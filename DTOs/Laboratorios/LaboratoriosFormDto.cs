using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Laboratorios
{
    public class LaboratoriosFormDto
    {
        [Required(ErrorMessage = "El laboratorio es requerido")]
        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("laboratorio")]
        public string laboratorio { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La direccion es requerida")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("direccion")]
        public string direccion { get; set; } = string.Empty;
    }
}
