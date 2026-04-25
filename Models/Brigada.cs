namespace sigevet.Models
{
    public class Brigada : Auditable
    {
        public int idBrigada { get; set; }
        public required String nombreBrigada { get; set; }
        public DateOnly fechaBrigada { get; set; }
        public TimeOnly horaInicio { get; set; }
        public TimeOnly horaFin { get; set; }
        public required String ubicacion {  get; set; }
        public String? cobertura { get; set; }
        public String? observaciones { get; set; }

        // Foránea
        public int idEstadoBrigada { get; set; }

        // Relaciones
        public Estado estadoBrigada { get; set; } = null!;
        public ICollection<MovimientoInventario> movimientosInventarios { get; set; } = new List<MovimientoInventario>();
        public ICollection<BrigadaVeterinario> veterinariosBrigada { get; set; } = new List<BrigadaVeterinario>();
    }
}
