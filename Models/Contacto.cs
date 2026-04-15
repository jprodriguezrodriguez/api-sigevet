namespace sigevet.Models
{
    public class Contacto: Auditable
    {
        public int idContacto {  get; set; }
        public required String detalleContacto { get; set; }

        // Nombre llaves foráneas
        public int idPersonaContacto { get; set; }
        public int idTipoContacto { get; set; }
        public int idEstadoContacto { get; set; }

        // Relaciones
        public Persona persona { get; set; } = null!;
        public TipoContacto tipoContacto { get; set; } = null!;
        public Estado estadoContacto { get; set; } = null!;

    }
}
