using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Vacunas
{
    public class VacunasFormDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El numero de lote es requerido")]
        [StringLength(12, ErrorMessage = "Maximo 12 caracteres")]
        [JsonPropertyName("numeroLote")]
        public string numeroLote { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de fabricacion es requerida")]
        [JsonPropertyName("fechaFabricacion")]
        public DateTime fechaFabricacion { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
        [JsonPropertyName("fechaVencimiento")]
        public DateTime fechaVencimiento { get; set; }

        [Required(ErrorMessage = "El tipo de vacuna es requerido")]
        [JsonPropertyName("idTipoVacuna")]
        public int idTipoVacuna { get; set; }

        [Required(ErrorMessage = "El laboratorio es requerido")]
        [JsonPropertyName("idLaboratorio")]
        public int idLaboratorio { get; set; }

        [Required(ErrorMessage = "El estado de la vacuna es requerido")]
        [JsonPropertyName("idEstadoVacuna")]
        public int idEstadoVacuna { get; set; }
    }
}
