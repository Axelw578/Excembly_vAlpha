namespace Excembly_vAlpha.ViewModels
{
    public class ContratacionViewModel
    {
        public int ContratacionId { get; set; }
        public int UsuarioId { get; set; }

        // Plan relacionado, se considera como nullable si no está seleccionado
        public int? PlanId { get; set; } // Plan seleccionado para la contratación
        public List<int> ServiciosAdicionalesIds { get; set; } = new List<int>(); // IDs de los servicios adicionales seleccionados
        public DateTime FechaContratacion { get; set; } = DateTime.MinValue; // Fecha de la contratación
        public string Estado { get; set; } // Estado de la contratación

        // Detalles del Plan
        public string NombrePlan { get; set; } = "Plan no especificado";
        public decimal PrecioPlan { get; set; } = 0;

        // Servicios incluidos en el plan
        public List<ServicioViewModel> ServiciosIncluidos { get; set; } = new List<ServicioViewModel>();

        // Servicios adicionales seleccionados para la contratación
        public List<ServicioAdicionalViewModel> ServiciosAdicionalesSeleccionados { get; set; } = new List<ServicioAdicionalViewModel>();

        // Servicios adicionales disponibles para la contratación
        public List<ServicioAdicionalViewModel> ServiciosAdicionalesDisponibles { get; set; } = new List<ServicioAdicionalViewModel>();

        // Planes disponibles para contratación
        public List<PlanViewModel> Planes { get; set; } = new List<PlanViewModel>();

        // Servicios adicionales contratados
        public List<ServicioAdicionalViewModel> ServiciosAdicionalesContratados { get; set; } = new List<ServicioAdicionalViewModel>();

        // Datos del técnico asignado
        public string NombreTecnico { get; set; } = "Técnico no asignado";
        public string FotoTecnicoUrl { get; set; } = "/images/default-user.png"; // Ruta por defecto para una imagen genérica

        // Dirección del domicilio
        public string DireccionDomicilio { get; set; } = "No especificada";

        // Nueva funcionalidad: contratación de un servicio individual
        public int? ServicioIndividualId { get; set; } // ID del servicio individual seleccionado
        public ServicioViewModel ServicioIndividual { get; set; } = new ServicioViewModel(); // Detalles del servicio individual
        public List<ServicioViewModel> ServiciosIndividualesDisponibles { get; set; } = new List<ServicioViewModel>();

        // Nueva propiedad para determinar si un plan está seleccionado
        public bool EsPlanSeleccionado => PlanId.HasValue && PlanId > 0;

        // Nueva propiedad para determinar si se ha seleccionado algún servicio individual
        public bool EsServicioIndividualSeleccionado => ServicioIndividualId.HasValue && ServicioIndividualId > 0;
    }
}