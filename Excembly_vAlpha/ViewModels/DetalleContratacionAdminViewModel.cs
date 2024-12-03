namespace Excembly_vAlpha.ViewModels
{
    public class DetalleContratacionAdminViewModel
    {
        // Información básica de la contratación
        public int ContratacionId { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Estado { get; set; }

        // Información del usuario
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }

        // Información del servicio
        public string ServicioContratado { get; set; }
        public string PlanContratado { get; set; }
        public List<string> ServiciosAdicionalesContratados { get; set; }

        // Información de pagos
        public List<PagoAdminViewModel> Pagos { get; set; }
    }
}
