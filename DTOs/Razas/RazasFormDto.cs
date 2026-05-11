using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Razas
{
    public class RazasFormDto
    {
        [Required(ErrorMessage = "La raza es requerida")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("raza")]
        public string raza { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }

        [Required(ErrorMessage = "La especie es requerida")]
        [JsonPropertyName("idEspecie")]
        public int idEspecie { get; set; }
    }
}
