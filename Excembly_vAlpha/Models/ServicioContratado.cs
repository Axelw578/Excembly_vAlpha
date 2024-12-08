using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excembly_vAlpha.Models
{
    public class ServicioContratado
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Contratacion")]
        public int ContratacionId { get; set; }
        public Contratacion Contratacion { get; set; } = null!;

        [ForeignKey("Servicio")]
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; } = null!;
    }
}
