using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Contactos
{
    public class ContactosFormDto
    {
        [Required(ErrorMessage = "El detalle de contacto es requerido")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("detalleContacto")]
        public string detalleContacto { get; set; } = string.Empty;

        [JsonPropertyName("idPersonaContacto")]
        public int? idPersonaContacto { get; set; }

        [JsonPropertyName("idLaboratorioContacto")]
        public int? idLaboratorioContacto { get; set; }

        [Required(ErrorMessage = "El tipo de contacto es requerido")]
        [JsonPropertyName("idTipoContacto")]
        public int idTipoContacto { get; set; }

        [Required(ErrorMessage = "El estado de contacto es requerido")]
        [JsonPropertyName("idEstadoContacto")]
        public int idEstadoContacto { get; set; }
    }
}
