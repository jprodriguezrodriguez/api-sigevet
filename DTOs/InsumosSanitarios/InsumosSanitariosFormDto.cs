using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.InsumosSanitarios
{
    public class InsumosSanitariosFormDto
    {
        [Required(ErrorMessage = "El insumo sanitario es requerido")]
        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("insumoSanitario")]
        public string insumoSanitario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("descripcion")]
        public string descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de insumo es requerido")]
        [JsonPropertyName("idTipoInsumo")]
        public int idTipoInsumo { get; set; }

        [Required(ErrorMessage = "La unidad de medida es requerida")]
        [JsonPropertyName("idUnidadMedida")]
        public int idUnidadMedida { get; set; }

        [Required(ErrorMessage = "El estado del insumo es requerido")]
        [JsonPropertyName("idEstadoInsumo")]
        public int idEstadoInsumo { get; set; }
    }
}
