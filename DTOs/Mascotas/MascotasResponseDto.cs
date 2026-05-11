namespace sigevet.DTOs.Mascotas
{
    public class MascotasResponseDto
    {
        public int idMascota { get; set; }
        public string? nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public char sexo { get; set; }
        public string? color { get; set; }
        public decimal peso { get; set; }
        public string? seniasParticulares { get; set; }
        public int idRaza { get; set; }
        public int idEstadoMascota { get; set; }
        public string? raza { get; set; }
        public string? estadoMascota { get; set; }
        public List<string>? tutores { get; set; }
    }
}
