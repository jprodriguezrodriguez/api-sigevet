using sigevet.DTOs.EsquemasVacunacion;
using sigevet.DTOs.Mascotas;
using sigevet.DTOs.UnidadesMedida;

namespace sigevet.DTOs.Vacunaciones
{
    public class VacunacionesResponseDto
    {
        public int? idVacunacion { get; set; }
        public DateTime fechaAplicacion { get; set; }
        public decimal dosisAplicada { get; set; }
        public int numeroDosis { get; set; }
        public string? observaciones { get; set; }
        public DateOnly? proximaFecha { get; set; }
        public int idEsquemaVacunacion { get; set; }
        public int idMascota { get; set; }
        public int idUnidadMedida { get; set; }
        public EsquemasVacunacionResponseDto? esquemasVacunacion { get; set; }
        public MascotasResponseDto? mascotas { get; set; }
        public UnidadesMedidaResponseDto? unidadesMedida { get; set; }
    }
}
