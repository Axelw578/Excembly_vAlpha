namespace Excembly_vAlpha.ViewModels
{
    public class ContratacionViewModel
    {
        public int ContratacionId { get; set; }
        public int UsuarioId { get; set; }
        public int PlanId { get; set; } // Plan seleccionado para la contratación
        public List<int> ServiciosAdicionalesIds { get; set; } // IDs de los servicios adicionales seleccionados
        public DateTime FechaContratacion { get; set; } // Fecha de la contratación
        public string Estado { get; set; } // Estado de la contratación

        // Detalles del Plan
        public string NombrePlan { get; set; }
        public decimal PrecioPlan { get; set; }

        // Servicios incluidos en el plan (relacionados a través de PlanServicio)
        public List<ServicioViewModel> ServiciosIncluidos { get; set; }

        // Servicios adicionales disponibles (relacionados con el Plan)
        public List<ServicioAdicionalViewModel> ServiciosAdicionalesDisponibles { get; set; }

        // Lista de planes disponibles (si deseas mostrar más de un plan en el proceso de contratación)
        public List<PlanViewModel> Planes { get; set; }

        // Propiedades nuevas para mostrar el técnico asignado y la dirección del domicilio
        public List<ServicioAdicionalViewModel> ServiciosAdicionalesContratados { get; set; } // Servicios adicionales contratados

        // Datos del técnico
        public string NombreTecnico { get; set; }
        public string FotoTecnicoUrl { get; set; }

        // Dirección del domicilio
        public string DireccionDomicilio { get; set; }

        // Nueva funcionalidad: contratación de un servicio individual
        public int ServicioIndividualId { get; set; } // ID del servicio individual seleccionado
        public ServicioViewModel ServicioIndividual { get; set; } // Detalles del servicio individual
        public List<ServicioViewModel> ServiciosIndividualesDisponibles { get; set; }

    }
}
