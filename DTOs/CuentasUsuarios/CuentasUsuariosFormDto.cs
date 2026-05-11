using sigevet.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace sigevet.DTOs.CuentasUsuarios
{
    public class CuentasUsuariosFormDto
    {
        [Required(ErrorMessage = "El id de la persona es requerido")]
        [JsonPropertyName("idPersona")]
        public int idPersona { get; set; }

        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [JsonPropertyName("usuario")]
        public string usuario{ get; set; } = string.Empty;

        [Required(ErrorMessage = "Contraseña es requerida")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [JsonPropertyName("constrasenia")]
        public string constrasenia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es requerido")]
        [JsonPropertyName("idRol")]
        public int idRol {  get; set; }
    }
}
