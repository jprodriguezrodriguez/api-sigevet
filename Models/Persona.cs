using System.Diagnostics.Contracts;

namespace sigevet.Models
{
    public class Persona : Auditable
    {
        public int idPersona { get; set; }
        public required String primerNombre { get; set; }
        public String? segundoNombre { get; set; }
        public required String primerApellido { get; set; }
        public String? segundoApellido { get; set; }
        public required String numeroIdentificacion { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public required String direccion { get; set; }

        // Nombre de las foráneas
        public int idTipoIidentificacion { get; set; }
        public int idEstadoPersona { get; set; }

        // Relaciones
        public required TipoIdentificacion tipoIdentificacion { get; set; }
        public required Estado estadoPersona { get; set; }
        public required ICollection<Contacto> contactosPersona { get; set; } = new List<Contacto>();

    }
}
