namespace Excembly_vAlpha.ViewModels
{
    public class PagoAdminViewModel
    {
        public int PagoId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }

        // Información del usuario que realizó el pago
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }

        // Relación con la contratación
        public int? ContratacionId { get; set; }
    }

}
