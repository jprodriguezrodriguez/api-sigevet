using Microsoft.EntityFrameworkCore;

namespace sigevet.Models
{
    public class Inventario: Auditable
    {
        public int idInventario {  get; set; }
        public int cantidadDisponible { get; set; }
        public int stockMinimo { get; set; }

        // Foráneas
        public int idInsumoSanitario { get; set; }

        // Relaciones
        public InsumoSanitario insumoSanitario { get; set; } = null!;
        public ICollection<MovimientoInventario> movimientosInventario { get; set; } = new List<MovimientoInventario>();
    }
}
