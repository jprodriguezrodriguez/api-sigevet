namespace sigevet.Models
{
    public class Laboratorio : Auditable
    {
        public int idLaboratorio {  get; set; }
        public required String laboratorio { get; set; }
        public String? descripcion { get; set; }
        public String? direccion { get; set; }

        // Relaciones
        public ICollection<Contacto> contactosLaboratorio { get; set; } = new List<Contacto>();
    }
}
