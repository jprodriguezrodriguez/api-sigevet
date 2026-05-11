namespace sigevet.DTOs.Inventarios
{
    public class InventariosResponseDto
    {
        public int idInventario { get; set; }
        public int cantidadDisponible { get; set; }
        public int stockMinimo { get; set; }
        public string? insumoSanitario { get; set; }
    }
}
