using System;

namespace Excembly_vAlpha.Models
{
    public class HistorialUsoPlan
    {
        public int HistorialId { get; set; }
        public int PlanId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaUso { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public string CodigoServicio { get; set; }
        public bool Usado { get; set; } = false;

        // Relaciones
        public Plan Plan { get; set; }
        public Usuario Usuario { get; set; }
    }
}
