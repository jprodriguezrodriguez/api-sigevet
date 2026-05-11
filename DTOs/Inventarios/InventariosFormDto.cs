using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Inventarios
{
    public class InventariosFormDto
    {
        [Required(ErrorMessage = "La cantidad disponible es requerida")]
        [JsonPropertyName("cantidadDisponible")]
        public int cantidadDisponible { get; set; }

        [Required(ErrorMessage = "El stock minimo es requerido")]
        [JsonPropertyName("stockMinimo")]
        public int stockMinimo { get; set; }

        [Required(ErrorMessage = "El insumo sanitario es requerido")]
        [JsonPropertyName("idInsumoSanitario")]
        public int idInsumoSanitario { get; set; }
    }
}
