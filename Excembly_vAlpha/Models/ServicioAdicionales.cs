using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excembly_vAlpha.Models
{
    // actualizado
    [Table("serviciosadicionales")]
    public class ServicioAdicionales
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int ServicioId { get; set; }
        public decimal Descuento { get; set; } = 0.10m;

        // Relaciones
        public Plan Plan { get; set; }
        public Servicio Servicio { get; set; }

        public string Nombre => Servicio?.Nombre ?? "Servicio desconocido";

        public ICollection<ServicioAdicionalContratado> ServiciosAdicionalesContratados { get; set; }
    }

}
