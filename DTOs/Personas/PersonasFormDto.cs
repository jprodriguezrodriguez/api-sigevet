using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Personas
{
    public class PersonasFormDto
    {
        [Required(ErrorMessage = "El primer nombre es requerido")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("primerNombre")]
        public string primerNombre { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("segundoNombre")]
        public string? segundoNombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("primerApellido")]
        public string primerApellido { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [JsonPropertyName("segundoApellido")]
        public string? segundoApellido { get; set; }

        [Required(ErrorMessage = "El numero de identificacion es requerido")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        [JsonPropertyName("numeroIdentificacion")]
        public string numeroIdentificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [JsonPropertyName("fechaNacimiento")]
        public DateTime fechaNacimiento { get; set; }

        [Required(ErrorMessage = "La direccion es requerida")]
        [StringLength(150, ErrorMessage = "Maximo 150 caracteres")]
        [JsonPropertyName("direccion")]
        public string direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de identificacion es requerido")]
        [JsonPropertyName("idTipoIdentificacion")]
        public int idTipoIdentificacion { get; set; }

        [Required(ErrorMessage = "El estado de la persona es requerido")]
        [JsonPropertyName("idEstadoPersona")]
        public int idEstadoPersona { get; set; }
    }
}
