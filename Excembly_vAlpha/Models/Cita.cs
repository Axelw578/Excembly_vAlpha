using System;

namespace Excembly_vAlpha.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int PlanId { get; set; }
        public int TecnicoId { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string Estado { get; set; } = "pendiente"; // Default
        public string Comentarios { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Plan Plan { get; set; }
        public Tecnico Tecnico { get; set; }
    }
}
