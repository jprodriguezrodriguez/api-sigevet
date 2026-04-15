namespace sigevet.Models
{
    public class TipoContacto
    {
        public required int idTipoContacto { get; set; }
        public required String tipoContacto { get; set; }
        public String? descripcion { get; set; }
    }
}
