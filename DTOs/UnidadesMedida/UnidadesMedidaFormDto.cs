using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.UnidadesMedida
{
    public class UnidadesMedidaFormDto
    {
        [Required(ErrorMessage = "La unidad de medida es requerida")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("unidadMedida")]
        public string unidadMedida { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
    }
}
