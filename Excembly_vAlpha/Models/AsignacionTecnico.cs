using System;

namespace Excembly_vAlpha.Models
{
    public class AsignacionTecnico
    {

        public int AsignacionId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public int ServicioId { get; set; }
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "pendiente"; // Defecto

        // Relaciones
        public Usuario Usuario { get; set; }
        public Tecnico Tecnico { get; set; }
        public Servicio Servicio { get; set; }
    }
}
