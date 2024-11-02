namespace Excembly_vAlpha.Models
{
    public class PlanServicio
    {
        public int PlanId { get; set; }
        public int ServicioId { get; set; }

        public Plan Plan { get; set; }
        public Servicio Servicio { get; set; }
    }

}
