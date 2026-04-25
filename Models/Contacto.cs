namespace sigevet.Models
{
    public class Contacto: Auditable
    {
        public int idContacto {  get; set; }
        public required String detalleContacto { get; set; }

        // Foráneas
        public int? idPersonaContacto { get; set; }
        public int? idLaboratorioContacto { get; set; }
        public int idTipoContacto { get; set; }
        public int idEstadoContacto { get; set; }

        // Relaciones
        public Persona? persona { get; set; }
        public Laboratorio? laboratorio { get; set; }
        public TipoContacto tipoContacto { get; set; } = null!;
        public Estado estadoContacto { get; set; } = null!;

    }
}
