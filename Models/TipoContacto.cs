namespace sigevet.Models
{
    public class TipoContacto : Auditable
    {
        public int idTipoContacto { get; set; }
        public required String tipoContacto { get; set; }
        public String? descripcion { get; set; }
    }
}
