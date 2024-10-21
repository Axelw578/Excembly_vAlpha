using System;

namespace Excembly_vAlpha.Models
{
    public class BitacoraEvento
    {
        public int EventoId { get; set; }
        public int UsuarioId { get; set; }
        public string DescripcionEvento { get; set; }
        public string TipoEvento { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string IPUsuario { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
    }
}
