using Excembly_vAlpha.Services;
namespace Excembly_vAlpha.Models
{
    // actualizado
    public class Plan
    {
        public int PlanId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }

        // Propiedad de navegación para los servicios del plan
        public ICollection<PlanServicio> PlanServicios { get; set; }

        // Relación con los servicios adicionales
        public ICollection<ServicioAdicional> ServiciosAdicionales { get; set; }

        // Relación con citas para control de contrataciones específicas
        public ICollection<Cita> Citas { get; set; }

        // Relación con contrataciones


        public ICollection<Contratacion> Contrataciones { get; set; }

        // Relación con dispositivos de plan familiar
        public ICollection<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; } 
    }
}
