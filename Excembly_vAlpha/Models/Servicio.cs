using Excembly_vAlpha.Services;

namespace Excembly_vAlpha.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int? TipoServicioId { get; set; }
        public bool ExclusivoPaquete { get; set; }

        public TipoServicio TipoServicio { get; set; }
        public ICollection<PlanServicio> PlanServicios { get; set; }
        public ICollection<ServicioAdicional> ServiciosAdicionales { get; set; }
        public ICollection<PlanPersonalizado> PlanesPersonalizados { get; set; }
        public ICollection<AsignacionTecnico> AsignacionesTecnicos { get; set; }
        public ICollection<Trabajo> Trabajos { get; set; }
        public bool EsIndividual => !ExclusivoPaquete; // Ejemplo: si no es exclusivo de un paquete, es individual


    }

}
