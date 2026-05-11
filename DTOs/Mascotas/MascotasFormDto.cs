using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sigevet.DTOs.Mascotas
{
    public class MascotasFormDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(60, ErrorMessage = "Maximo 60 caracteres")]
        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [JsonPropertyName("fechaNacimiento")]
        public DateTime fechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sexo es requerido")]
        [JsonPropertyName("sexo")]
        public char sexo { get; set; }

        [Required(ErrorMessage = "El color es requerido")]
        [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
        [JsonPropertyName("color")]
        public string color { get; set; } = string.Empty;

        [Required(ErrorMessage = "El peso es requerido")]
        [JsonPropertyName("peso")]
        public decimal peso { get; set; }

        [Required(ErrorMessage = "Las senias particulares son requeridas")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [JsonPropertyName("seniasParticulares")]
        public string seniasParticulares { get; set; } = string.Empty;

        [Required(ErrorMessage = "La raza es requerida")]
        [JsonPropertyName("idRaza")]
        public int idRaza { get; set; }

        [Required(ErrorMessage = "El estado de la mascota es requerido")]
        [JsonPropertyName("idEstadoMascota")]
        public int idEstadoMascota { get; set; }
    }
}
