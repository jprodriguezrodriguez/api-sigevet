namespace sigevet.Models
{
    public class Roles: Auditable
    {
        public required int idRol { get; set; }
        public required string rolUsuario { get; set; }
        public string? descripcion { get; set; }

        // Relaciones
        public ICollection<CuentasUsuarios> cuentasUsuariosPorRol { get; set; } = new List<CuentasUsuarios>();
    }
}
