using Microsoft.OpenApi.Writers;
using System.Text.Json.Serialization;

namespace sigevet.Models
{
    public class EsquemaVacunacion : Auditable
    {
        public int idEsquemaVacunacion {  get; set; }
        public required String esquemaVacunacion { get; set; }
        public int intervaloDias { get; set; }
        public int? edadMinimaDias { get; set; }
        public String? observaciones {  get; set; }

        // Foráneas
        public int idTipoVacuna { get; set; }

        // Relaciones
        public TipoVacuna? tipoVacuna { get; set; }
        [JsonIgnore]
        public ICollection<Vacunacion> vacunaciones { get; set; } = new List<Vacunacion>();
    }
}
