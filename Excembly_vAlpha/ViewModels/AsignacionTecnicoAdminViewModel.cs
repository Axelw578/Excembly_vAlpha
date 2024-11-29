namespace Excembly_vAlpha.ViewModels
{
    public class AsignacionTecnicoAdminViewModel
    {
        public int AsignacionId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Estado { get; set; }

        // Información del técnico asignado
        public int TecnicoId { get; set; }
        public string NombreTecnico { get; set; }
        public string ApellidoTecnico { get; set; }

        // Información de la contratación
        public int ContratacionId { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string EstadoContratacion { get; set; }
    }

}
