using System.Text.Json.Serialization;

namespace sigevet.Models
{
    public class CuentasUsuarios : Auditable
    {
        public required int idCuentaUsuario { get; set; }
        public required string username { get; set; }
        public required string passwordHash { get; set; }
        public DateTime? ultimoInicioSesion { get; set; }
        public int intentosFallidos { get; set; }
        public DateTime? fechaDesbloqueo { get; set; }

        // Foráneas
        public int idPersona { get; set; }
        public int idEstadoCuenta { get; set; }
        public int idRol { get; set; }

        // Relaciones
        [JsonIgnore]
        public Persona? persona { get; set; }
        public Estado? estadoCuenta { get; set; }
        public Roles? rolesUsuario { get; set; }
        public ICollection<RefreshToken> refreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<TokensCuentas> tokensCuentas { get; set; } = new List<TokensCuentas>();

    }
}
