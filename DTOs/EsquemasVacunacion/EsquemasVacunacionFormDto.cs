using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.EsquemasVacunacion
{
    public class EsquemasVacunacionFormDto
    {
        [Required(ErrorMessage = "El esquema de vacunacion es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [JsonPropertyName("esquemaVacunacion")]
        public string esquemaVacunacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El intervalo en dias es requerido")]
        [JsonPropertyName("intervaloDias")]
        public int intervaloDias { get; set; }

        [JsonPropertyName("edadMinimaDias")]
        public int? edadMinimaDias { get; set; }

        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("observaciones")]
        public string? observaciones { get; set; }

        [Required(ErrorMessage = "El tipo de vacuna es requerido")]
        [JsonPropertyName("idTipoVacuna")]
        public int idTipoVacuna { get; set; }
    }
}
