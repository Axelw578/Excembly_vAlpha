using System;

namespace Excembly_vAlpha.Models
{
    public class Trabajo
    {
        public int TrabajoId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public string Imagenes { get; set; }
        public string Comentarios { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Tecnico Tecnico { get; set; }
    }
}
