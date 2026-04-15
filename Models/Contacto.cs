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
        public required Persona persona { get; set; }
        public required TipoContacto tipoContacto { get; set; }
        public required Estado estadoContacto { get; set; }

    }
}
