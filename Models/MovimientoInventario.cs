namespace sigevet.Models
{
    public class MovimientoInventario : Auditable
    {
        public int idMovimientoInventario { get; set; }
        public int cantidad { get; set; }
        public DateTime fechaMovimiento {  get; set; }
        public String? motivo { get; set; }
        public String? observaciones { get; set; }

        // Foráneas
        public int idTipoMovimiento { get; set; }
        public int idResponsable { get; set; }
        public int idInventario { get; set; }
        public int? idBrigada { get; set; }

        // Relaciones
        public TipoMovimiento tipoMovimiento { get; set; } = null!;
        public Persona responsableMovimiento { get; set; } = null!;
        public Inventario inventario {  get; set; } = null!;
        public Brigada? brigada { get; set; } 
    }
}
