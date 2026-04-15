namespace sigevet.Models
{
    public class TipoIdentificacion : Auditable
    {
        public int idTipoIdentificacion {  get; set; }
        public required String tipoIdentificacion { get; set; }
        public String? descripcion { get; set; }

        public required ICollection<Persona> personas { get; set; }

        public void registrarTipoIdentificacion(String nombreTipoIdentificacion)
        {
        }

        public void actualizarTipoIdentificacion(int idTipoIdentificacion)
        {
        }
    }
}
