namespace Excembly_vAlpha.ViewModels
{
    public class ContratacionAdminViewModelDTO
    {
        public int ContratacionId { get; set; }
        public DateTime FechaContratacion { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public PlanDTO Plan { get; set; }
        public List<ServicioAdicionalDTO> ServiciosAdicionales { get; set; }
    }

    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
    }

    public class PlanDTO
    {
        public int PlanId { get; set; }
        public string Nombre { get; set; }
    }

    public class ServicioAdicionalDTO
    {
        public int ServicioAdicionalId { get; set; }
        public string Nombre { get; set; }
    }

}
