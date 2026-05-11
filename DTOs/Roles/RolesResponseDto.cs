using sigevet.Models;

namespace sigevet.DTOs.Roles
{
    public class RolesResponseDto
    {
        public int idRol { get; set; }
        public String rolUsuario { get; set; } = string.Empty;
        public String? descripcion { get; set; }
    }
}
