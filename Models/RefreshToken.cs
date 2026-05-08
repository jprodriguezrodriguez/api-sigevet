namespace sigevet.Models
{
    public class RefreshToken
    {
        public int idRefreshToken { get; set; }
        public string tokenHash { get; set; } = null!;
        public DateTime fechaCreacion { get; set; } = DateTime.Now;
        public DateTime fechaExpiracion { get; set; }
        public DateTime? fechaRevocacion { get; set; }
        public string? ip { get; set; }
        public string? userAgent { get; set; }

        // foránea
        public int idCuentaUsuario { get; set; }
        // Relación
        public CuentasUsuarios? tokensPorCuentaUsuario { get; set; } = null!;

        //Métodos para verificar el estado del token
        public bool estaExpirado()
        {
            return DateTime.UtcNow >= fechaExpiracion;
        }

        public bool estaRevocado()
        {
            return fechaRevocacion != null;
        }
        public bool estaActivo()
        {
            return !estaExpirado() && !estaRevocado();
        }
        public void revocar()
        {
            fechaRevocacion = DateTime.UtcNow;
        }
    }
}
