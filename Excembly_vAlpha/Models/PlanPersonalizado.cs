namespace Excembly_vAlpha.Models
{
    public class PlanPersonalizado
    {
        public int UsuarioId { get; set; }
        public int CitaId { get; set; }
        public int ServicioId { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;

        public Usuario Usuario { get; set; }
        public Cita Cita { get; set; }
        public Servicio Servicio { get; set; }
    }

}
