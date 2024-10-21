namespace Excembly_vAlpha.Models
{
    public class Plan_Servicio
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
    }
}
