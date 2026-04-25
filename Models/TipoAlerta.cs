namespace sigevet.Models
{
    public class TipoAlerta : Auditable
    {
        public int idTipoAlerta { get; set; }
        public required String alerta { get; set; }
        public String? descripcion { get; set; }

        // Relaciones
        public ICollection<AlertaVacunacion> alertasVacunacion {  get; set; } = new List<AlertaVacunacion>();
    }
}
