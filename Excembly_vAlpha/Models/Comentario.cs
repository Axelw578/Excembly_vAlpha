using System;

namespace Excembly_vAlpha.Models
{
    // modelo actualizado
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public int UsuarioId { get; set; }
        public int? ServicioId { get; set; }
        public int? PlanId { get; set; }
        public int? PlanPersonalizadoId { get; set; }
        public int? ContratacionId { get; set; }  
        public string Opinion { get; set; }
        public DateTime FechaComentario { get; set; } = DateTime.Now;
        public string FotoUrl { get; set; }  // Opcional: URL de la foto

        // Relaciones
        public Usuario Usuario { get; set; }
        public Servicio Servicio { get; set; }
        public Plan Plan { get; set; }
        public PlanPersonalizado PlanPersonalizado { get; set; }
        public Contratacion Contratacion { get; set; }  // Relación con Contratación
    }
}
