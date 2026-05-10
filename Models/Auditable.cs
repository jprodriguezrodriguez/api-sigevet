namespace sigevet.Models
{
    public class Auditable
    {
        public DateTime? fechaCreacion {  get; set; }
        public DateTime? fechaActualizacion { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
