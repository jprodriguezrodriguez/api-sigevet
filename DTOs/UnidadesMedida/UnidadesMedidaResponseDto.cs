using sigevet.Models;

namespace sigevet.DTOs.UnidadesMedida
{
    public class UnidadesMedidaResponseDto
    {
        public int idUnidadMedida { get; set; }
        public string unidadMedida { get; set; } = string.Empty;
        public string? descripcion { get; set; } = null;
    }
}
