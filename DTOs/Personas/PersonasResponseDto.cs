namespace sigevet.DTOs.Personas
{
    public class PersonasResponseDto
    {
        public int idPersona { get; set; }
        public string primerNombre { get; set; } = string.Empty;
        public string? segundoNombre { get; set; }
        public string primerApellido { get; set; } = string.Empty;
        public string? segundoApellido { get; set; }
        public string numeroIdentificacion { get; set; } = string.Empty;
        public DateTime fechaNacimiento { get; set; }
        public string direccion { get; set; } = string.Empty;
        public string? tipoIdentificacion { get; set; }
        public string? estadoPersona { get; set; }
    }
}
