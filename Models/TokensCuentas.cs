namespace sigevet.Models
{
    public class TokensCuentas: Auditable
    {
        public int idToken { get; set; }
        public string tokenHash { get; set; } = null!;
        public DateTime fechaExpiracion { get; set; }
        public DateTime? fechaUso { get; set; }
        public bool usado { get; set; }
        // Foránea
        public int idCuentaUsuario { get; set; }

        // Relación
        public CuentasUsuarios? tokenCuentaUsuario { get; set; } = null!;
    }
}
