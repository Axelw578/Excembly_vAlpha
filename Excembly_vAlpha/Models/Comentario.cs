using System;

namespace Excembly_vAlpha.Models
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public int UsuarioId { get; set; }
        public int ContratacionId { get; set; }  // Clave foránea a Contratacion
        public string Opinion { get; set; }
        public DateTime FechaComentario { get; set; } = DateTime.Now;
        public string FotoUrl { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Contratacion Contratacion { get; set; }  // Relación con Contratacion
    }
}
