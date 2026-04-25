namespace sigevet.Models
{
    public class AlertaVacunacion : Auditable
    {
        public int idAlerta {  get; set; }
        public DateOnly fechaGeneracion {  get; set; }
        public DateOnly fechaProgramada { get; set; }
        public required String mensaje {  get; set; }

        // Foráneas
        public int idTipoAlerta { get; set; }
        public int idVacunacion { get; set; }

        // Relaciones
        public TipoAlerta tipoAlerta { get; set; } = null!;
        public Vacunacion vacunaciones { get; set; } = null!;
    }
}
