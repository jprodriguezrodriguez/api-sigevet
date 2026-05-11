using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Vacunaciones
{
    public class VacunacionesFormDto
    {
        [Required(ErrorMessage = "La fecha de aplicacion es requerida")]
        [JsonPropertyName("fechaAplicacion")]
        public DateTime fechaAplicacion { get; set; }

        [Required(ErrorMessage = "La dosis aplicada es requerida")]
        [JsonPropertyName("dosisAplicada")]
        public decimal dosisAplicada { get; set; }

        [Required(ErrorMessage = "El numero de dosis es requerido")]
        [JsonPropertyName("numeroDosis")]
        public int numeroDosis { get; set; }

        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("observaciones")]
        public string? observaciones { get; set; }

        [Required(ErrorMessage = "El esquema de vacunacion es requerido")]
        [JsonPropertyName("idEsquemaVacunacion")]
        public int idEsquemaVacunacion { get; set; }

        [Required(ErrorMessage = "La mascota es requerida")]
        [JsonPropertyName("idMascota")]
        public int idMascota { get; set; }

        [Required(ErrorMessage = "La unidad de medida es requerida")]
        [JsonPropertyName("idUnidadMedida")]
        public int idUnidadMedida { get; set; }
    }
}
