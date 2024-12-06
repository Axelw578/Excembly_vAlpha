namespace Excembly_vAlpha.ViewModels
{
    public class PagoViewModel
    {
        public int UsuarioId { get; set; }
        public int? ContratacionId { get; set; }
        public int? ServicioId { get; set; }
        public int? PlanId { get; set; }
        public int? TarjetaId { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string Referencia { get; set; }

        // Información adicional para mostrar en la vista
        public string NombrePlan { get; set; }
        public string NombreServicio { get; set; }
        public string NombreTarjeta { get; set; }
        public decimal Total { get; set; }
    }

}
