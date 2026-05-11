using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.MovimientoInventarios
{
    public class MovimientoInventariosFormDto
    {
        [Required(ErrorMessage = "La cantidad es requerida")]
        [JsonPropertyName("cantidad")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "La fecha de movimiento es requerida")]
        [JsonPropertyName("fechaMovimiento")]
        public DateTime fechaMovimiento { get; set; }

        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("motivo")]
        public string? motivo { get; set; }

        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("observaciones")]
        public string? observaciones { get; set; }

        [Required(ErrorMessage = "El tipo de movimiento es requerido")]
        [JsonPropertyName("idTipoMovimiento")]
        public int idTipoMovimiento { get; set; }

        [Required(ErrorMessage = "El responsable es requerido")]
        [JsonPropertyName("idResponsable")]
        public int idResponsable { get; set; }

        [Required(ErrorMessage = "El inventario es requerido")]
        [JsonPropertyName("idInventario")]
        public int idInventario { get; set; }

        [JsonPropertyName("idBrigada")]
        public int? idBrigada { get; set; }
    }
}
