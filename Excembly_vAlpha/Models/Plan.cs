using Excembly_vAlpha.Services;

namespace Excembly_vAlpha.Models
{
    public class Plan
    {
        public int PlanId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }

        public ICollection<PlanServicio> PlanServicios { get; set; }
        public ICollection<ServicioAdicional> ServiciosAdicionales { get; set; }
        public ICollection<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; }
    }

}
