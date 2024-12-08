using System.Collections.Generic;

namespace Excembly_vAlpha.ViewModels
{
    public class ServicioContratadoViewModel
    {
        public int Id { get; set; }
        public int ContratacionId { get; set; }
        public int ServicioId { get; set; }
        public string NombreServicio { get; set; } = string.Empty;
        public decimal PrecioServicio { get; set; }
    }

    public class ContratacionConServiciosViewModel
    {
        public int ContratacionId { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string NombrePlan { get; set; } = string.Empty;
        public decimal? PrecioPlan { get; set; }
        public List<ServicioContratadoViewModel> ServiciosContratados { get; set; } = new();
    }
}
