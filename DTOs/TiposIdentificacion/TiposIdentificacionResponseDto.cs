using sigevet.Models;

namespace sigevet.DTOs.TiposIdentificacion
{
    public class TipoIdentificacionResponseDto
    {
        public int idTipoIdentificacion { get; set; }
        public String tipoIdentificacion { get; set; } = string.Empty;
        public String? descripcion { get; set; }
    }
}
