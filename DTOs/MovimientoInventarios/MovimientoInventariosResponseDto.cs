namespace sigevet.DTOs.MovimientoInventarios
{
    public class MovimientoInventariosResponseDto
    {
        public int idMovimientoInventario { get; set; }
        public int cantidad { get; set; }
        public DateTime fechaMovimiento { get; set; }
        public string? motivo { get; set; }
        public string? observaciones { get; set; }
        public string? tipoMovimiento { get; set; }
        public string? responsableMovimiento { get; set; }
        public string? insumoSanitario { get; set; }
        public string? brigada { get; set; }
    }
}
