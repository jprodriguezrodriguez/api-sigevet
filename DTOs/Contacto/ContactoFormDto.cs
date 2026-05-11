using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Contactos
{
    public class ContactoFormDto
    {
        [Required(ErrorMessage = "El detalle del contacto es requerido")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres")]
        [JsonPropertyName("detalleContacto")]
        public string detalleContacto { get; set; } = string.Empty;

        [JsonPropertyName("idPersonaContacto")]
        public int? idPersonaContacto { get; set; }

        [JsonPropertyName("idLaboratorioContacto")]
        public int? idLaboratorioContacto { get; set; }

        [Required(ErrorMessage = "El tipo de contacto es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de contacto válido")]
        [JsonPropertyName("idTipoContacto")]
        public int idTipoContacto { get; set; }

        [Required(ErrorMessage = "El estado del contacto es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado de contacto válido")]
        [JsonPropertyName("idEstadoContacto")]
        public int idEstadoContacto { get; set; }
    }
}