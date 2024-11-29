namespace Excembly_vAlpha.ViewModels
{
    public class ContratacionAdminViewModel
    {
        public int ContratacionId { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Estado { get; set; }
        public string TipoServicio { get; set; }

        // Información del usuario
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string TelefonoUsuario { get; set; }

        // Información adicional
        public string PlanContratado { get; set; } // Nombre del plan, si aplica
        public string ServicioContratado { get; set; } // Nombre del servicio, si aplica
    }

}
