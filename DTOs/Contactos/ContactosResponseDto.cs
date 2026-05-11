namespace sigevet.DTOs.Contactos
{
    public class ContactosResponseDto
    {
        public int idContacto { get; set; }
        public string detalleContacto { get; set; } = string.Empty;
        public int? idPersonaContacto { get; set; }
        public int? idLaboratorioContacto { get; set; }
        public string? tipoContacto { get; set; }
        public string? estadoContacto { get; set; }
    }
}
