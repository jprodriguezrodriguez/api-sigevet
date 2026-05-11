namespace sigevet.DTOs.TiposContacto
{
    public class TiposContactoResponseDto
    {
        public int idTipoContacto { get; set; }
        public string tipoContacto { get; set; } = string.Empty;
        public string? descripcion { get; set; }
    }
}
