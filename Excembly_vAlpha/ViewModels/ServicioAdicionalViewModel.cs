namespace Excembly_vAlpha.ViewModels
{
    public class ServicioAdicionalViewModel
    {
        public int ServicioId { get; set; } // ID único del servicio adicional
        public string NombreServicio { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioConDescuento { get; set; }
        public decimal Descuento { get; set; }
    }
}
