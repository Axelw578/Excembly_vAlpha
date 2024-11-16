using System;
using System.Collections.Generic;
using System.Linq;

namespace Excembly_vAlpha.ViewModels
{
    public class DetalleContratacionViewModel
    {
        public int ContratacionId { get; set; }
        public string Estado { get; set; }
        public DateTime FechaContratacion { get; set; }

        // Datos del Plan
        public int? PlanId { get; set; }
        public string NombrePlan { get; set; }
        public decimal? PrecioPlan { get; set; }

        // Servicios incluidos en el plan
        public List<ServicioViewModel> ServiciosIncluidos { get; set; } = new();

        // Servicios adicionales contratados (con descuento)
        public List<ServicioAdicionalViewModel> ServiciosAdicionalesContratados { get; set; } = new();

        // Servicios individuales contratados (sin descuento)
        public List<ServicioViewModel> ServiciosIndividualesContratados { get; set; } = new();

        // Precio total de la contratación (plan + servicios adicionales + servicios individuales)
        public decimal PrecioTotal =>
            (PrecioPlan ?? 0) +
            (ServiciosAdicionalesContratados?.Sum(s => s.PrecioConDescuento) ?? 0) +
            (ServiciosIndividualesContratados?.Sum(s => s.Precio) ?? 0);

        // Datos del Técnico asignado (si aplica)
        public int? TecnicoId { get; set; }
        public string NombreTecnico { get; set; }
        public string FotoTecnicoUrl { get; set; }

        // Información del domicilio si es un servicio a domicilio
        public string DireccionDomicilioCompleta { get; set; }
        public string Comentarios { get; set; }

        // Información específica para servicios en establecimiento
        public string NombreEstablecimiento { get; set; }
        public string HorarioEstablecimiento { get; set; }
        public string DireccionEstablecimiento { get; set; }
        public string ImagenMapaEstablecimientoUrl { get; set; }

        // Políticas de cancelación
        public string PoliticaCancelacion { get; set; }
    }
}
