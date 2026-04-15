namespace sigevet.Models
{
    public class Auditable
    {
        public DateTime fechaCreacion {  get; set; } = DateTime.Now; // Se añade un valor por defecto al atributo creado
        public DateTime fechaActualizacion { get; set; } = DateTime.Now; // Se añade un valor por defecto al atributo creado
    }
}
