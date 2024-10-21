namespace Excembly_vAlpha.ViewModels
{
    public class PagoViewModel
    {
        public int PagoId { get; set; }
        public int UsuarioId { get; set; }
        public int? CitaId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
    }
}
