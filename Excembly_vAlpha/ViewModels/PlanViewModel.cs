namespace Excembly_vAlpha.ViewModels
{
    public class PlanViewModel
    {
        public int PlanId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }

        // Lista de nombres de servicios incluidos en el plan
        public List<string> ServiciosIncluidos { get; set; }

        // Lista de servicios adicionales con sus descuentos aplicados
        public List<ServicioAdicionalViewModel> ServiciosAdicionales { get; set; }
    }

    public class ServicioAdicionalViewModel
    {
        public int ServicioId { get; set; } // ID único del servicio adicional
        public string NombreServicio { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioConDescuento { get; set; }
        public decimal Descuento { get; set; }
    }
}
