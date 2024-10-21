using System;

namespace Excembly_vAlpha.Models
{
    public class CambioContratacion
    {
        public int CambioId { get; set; }
        public int UsuarioId { get; set; }
        public int PlanId { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string MotivoCambio { get; set; }
        public string Estado { get; set; } = "pendiente"; // Default

        // Relaciones
        public Usuario Usuario { get; set; }
        public Plan Plan { get; set; }
    }
}
