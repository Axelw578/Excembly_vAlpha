using System;

namespace Excembly_vAlpha.Models
{
    public class AsignacionTecnico
    {
        public int AsignacionId { get; set; }
        public int TecnicoId { get; set; }
        public int ContratacionId { get; set; } // Relación principal con Contratacion
        public int UsuarioId { get; set; }
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
        public string Estado { get; set; }

        // Propiedades de navegación
        public Tecnico Tecnico { get; set; }
        public Contratacion Contratacion { get; set; }
        public Usuario Usuario { get; set; } // Opcional, si deseas acceso rápido al usuario
    }
}
