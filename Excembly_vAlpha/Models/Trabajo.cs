using System;

namespace Excembly_vAlpha.Models
{
    public class Trabajo
    {
        public int TrabajoId { get; set; }
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaTrabajo { get; set; } = DateTime.Now;

        public Cita Cita { get; set; }
        public Usuario Usuario { get; set; }
        public Tecnico Tecnico { get; set; }
    }
}
