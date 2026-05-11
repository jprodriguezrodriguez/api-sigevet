using sigevet.DTOs.Vacunaciones;

namespace sigevet.DTOs.EsquemasVacunacion
{
    public class EsquemasVacunacionResponseDto
    {
        public int idEsquemaVacunacion { get; set; }
        public string esquemaVacunacion { get; set; } = string.Empty;
        public int intervaloDias { get; set; }
        public int? edadMinimaDias { get; set; }
        public string? observaciones { get; set; }
        public int idTipoVacuna { get; set; }
        public string? tipoVacuna { get; set; }
        public List<VacunacionesResponseDto> vacunaciones { get; set; } = new List<VacunacionesResponseDto>();
    }
}
