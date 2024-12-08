namespace Excembly_vAlpha.ViewModels
{
    public class ServicioAdicionalContratadoViewModel
    {
        public int Id { get; set; }

        // Clave foránea hacia Contratacion
        public int ContratacionId { get; set; }

        // Clave foránea hacia ServicioAdicional
        public int ServicioAdicionalId { get; set; }
        public int PlanId { get; set; }

        // Descuento aplicado al servicio adicional
        public decimal DescuentoAplicado { get; set; }

        // Información del servicio adicional
        public string ServicioAdicionalNombre { get; set; } // Nombre del servicio adicional
        public decimal ServicioAdicionalPrecio { get; set; } // Precio original del servicio adicional
    }
}
