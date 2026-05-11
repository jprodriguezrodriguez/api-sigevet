namespace sigevet.DTOs.Tutores
{
    public class TutoresResponseDto
    {
        public int idPersonaTut { get; set; }
        public bool autorizaNotificaciones { get; set; }
        public DateTime fechaRegistroTutor { get; set; }
        public DateTime fechaActualizacionTutor { get; set; }
        public string? persona { get; set; }
        public string? estadoTutor { get; set; }
    }
}
