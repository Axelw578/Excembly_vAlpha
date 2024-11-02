namespace Excembly_vAlpha.Models
{
    public class ServicioAdicional
    {
        public int PlanId { get; set; }
        public int ServicioId { get; set; }
        public decimal Descuento { get; set; } = 0.10m;

        public Plan Plan { get; set; }
        public Servicio Servicio { get; set; }
    }

}
