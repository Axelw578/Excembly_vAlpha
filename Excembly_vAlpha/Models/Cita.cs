using System;

namespace Excembly_vAlpha.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public DateTime FechaCita { get; set; }
        public string EstadoContratacion { get; set; }
        public bool Domicilio { get; set; }
        public string Comentarios { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaContratacion { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Tecnico Tecnico { get; set; }

        // Propiedad de navegación para PlanesPersonalizados
        public ICollection<PlanPersonalizado> PlanesPersonalizados { get; set; }

        // Propiedad de navegación para Pagos
        public ICollection<Pago> Pagos { get; set; }
    }

}
