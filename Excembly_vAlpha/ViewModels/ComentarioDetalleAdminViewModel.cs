namespace Excembly_vAlpha.ViewModels
{
    public class ComentarioDetalleAdminViewModel
    {
        public int ComentarioId { get; set; }
        public string ComentarioTexto { get; set; }
        public DateTime FechaComentario { get; set; }
        public string UsuarioNombre { get; set; }
        public string TecnicoNombre { get; set; }
        public string PlanNombre { get; set; }
        public string ServicioNombre { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string TecnicoDetalles { get; set; } // Detalles del técnico asignado
        public string DetallesContratacion { get; set; } // Detalles de la contratación
    }

}
