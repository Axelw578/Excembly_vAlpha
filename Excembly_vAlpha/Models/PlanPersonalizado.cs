namespace Excembly_vAlpha.Models
{
    // actualizado
    public class PlanPersonalizado
    {
        public int PlanPersonalizadoId { get; set; } // ID con auto-incremento
        public int UsuarioId { get; set; }
        public int CitaId { get; set; }
        public int ServicioId { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;

        // Relaciones
        public Usuario Usuario { get; set; }
        public Cita Cita { get; set; }
        public Servicio Servicio { get; set; }

        public ICollection<Contratacion> Contrataciones { get; set; }
    }
}
