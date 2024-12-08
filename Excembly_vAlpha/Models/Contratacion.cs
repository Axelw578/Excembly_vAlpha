using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excembly_vAlpha.Models
{
    public class Contratacion
    {
        public int ContratacionId { get; set; }
        public int UsuarioId { get; set; }
        public int? PlanId { get; set; }
        public int? ServicioId { get; set; }
        public int? PlanPersonalizadoId { get; set; }
        public int? CitaId { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Activa";
        public DateTime? FechaCancelacion { get; set; }
        public string TipoServicio { get; set; } = "Sucursal";

        // Relaciones
        public Usuario Usuario { get; set; }
        public Plan Plan { get; set; }
        public Servicio Servicio { get; set; }
        public Cita Cita { get; set; }// NO LO USAREMOS IGNORAR 
        public ICollection<ServicioAdicionalContratado> ServiciosAdicionalesContratados { get; set; } = new List<ServicioAdicionalContratado>();
        public ICollection<AsignacionTecnico> AsignacionesTecnico { get; set; } // Relación con AsignacionTecnico
        public ICollection<ServicioContratado> ServiciosContratados { get; set; } = new List<ServicioContratado>();



        public ICollection<Comentario> Comentarios { get; set; }
    }

}
