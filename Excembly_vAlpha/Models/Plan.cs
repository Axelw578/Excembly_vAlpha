namespace Excembly_vAlpha.Models
{
    public class Plan
    {
        public int PlanId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Duracion { get; set; }

        // Relacinn con Servicio
        public ICollection<Plan_Servicio> Servicios { get; set; }
        public ICollection<Cita> Citas { get; set; }
        public ICollection<HistorialUsoPlan> HistorialUsoPlanes { get; set; }
        public ICollection<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; }
        public ICollection<CambioContratacion> CambiosContratacion { get; set; }
    }
}
