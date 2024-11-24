using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.ViewModels
{
    public class ContratacionViewModel
    {
        public int ContratacionId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;  // Nombre del usuario
        public string CorreoUsuario { get; set; } = string.Empty;  // Correo del usuario

        // Información del plan contratado
        public int? PlanId { get; set; }
        public string NombrePlan { get; set; } = string.Empty;
        public decimal PrecioPlan { get; set; }

        // Información del servicio contratado (si aplica)
        public int? ServicioId { get; set; }
        public string NombreServicio { get; set; } = string.Empty;
        public decimal PrecioServicio { get; set; }

        // Información sobre los servicios adicionales contratados
        public IEnumerable<ServicioAdicionalViewModel> ServiciosAdicionalesContratados { get; set; } = new List<ServicioAdicionalViewModel>();

        // ** Propiedad para los servicios adicionales seleccionados (solo IDs) **
        public List<int> ServiciosAdicionalesSeleccionados { get; set; } = new List<int>();

        // Estado y fechas de la contratación
        public string Estado { get; set; } = "Activa";
        public DateTime FechaContratacion { get; set; } = DateTime.Now;
        public DateTime? FechaCancelacion { get; set; }
        public string TipoServicio { get; set; } = "Sucursal"; // Puede ser "Sucursal" o "Domicilio"

        // Nuevas propiedades: Listas de elementos disponibles
        public IEnumerable<PlanViewModel> PlanesDisponibles { get; set; } = new List<PlanViewModel>(); // Lista de planes disponibles
        public IEnumerable<ServicioViewModel> ServiciosDisponibles { get; set; } = new List<ServicioViewModel>(); // Lista de servicios disponibles
        public IEnumerable<ServicioAdicionalViewModel> ServiciosAdicionalesDisponibles { get; set; } = new List<ServicioAdicionalViewModel>(); // Lista de servicios adicionales disponibles
    }

}
