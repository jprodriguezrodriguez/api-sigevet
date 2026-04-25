namespace sigevet.Models
{
    public class RolParticipacion : Auditable
    {
        public int idRolParticipacion {  get; set; }
        public required String rolParticipacion { get; set; }
        public String? descripcion { get; set; }

        // Relaciones
        public ICollection<BrigadaVeterinario> brigadasVeterinario { get; set; } = new List<BrigadaVeterinario>();
    }
}
