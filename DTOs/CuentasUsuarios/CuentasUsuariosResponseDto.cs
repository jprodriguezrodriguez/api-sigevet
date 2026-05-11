namespace sigevet.DTOs.CuentasUsuarios
{
    public class CuentasUsuariosResponseDto
    {
        public int idCuentaUsuario { get; set; }
        public String usuario { get; set; } = string.Empty;
        public DateTime? ultimoInicioSesion { get; set; }
        public String nombreRol { get; set; } = string.Empty;
        public String EstadoCuenta { get; set; } = string.Empty;
        public String nombrePersona {  get; set; } = string.Empty;

    }
}
