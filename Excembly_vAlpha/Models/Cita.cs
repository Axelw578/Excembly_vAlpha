using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.Models
{
    // actualizada
    public class Cita
    {
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public int? DireccionId { get; set; }  // Referencia a la dirección de la cita
        public DateTime FechaCita { get; set; }
        public DateTime? FechaCitaModificada { get; set; }  // Registro de cambios en la fecha de la cita
        public string EstadoCita { get; set; } = "Programada";  // Estado inicial de la cita
        public bool Domicilio { get; set; }
        public string Comentarios { get; set; }
        public string Imagen { get; set; }

        // Referencia a la contratación asociada
        public int? ContratacionId { get; set; }
        public Contratacion Contratacion { get; set; }

        // Fecha y motivo de cancelación de la cita
        public DateTime? FechaCancelacion { get; set; }
        public string MotivoCancelacion { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Tecnico Tecnico { get; set; }
        public Direccion Direccion { get; set; }

        // Referencia al plan contratado
        public int? PlanId { get; set; }
        public Plan Plan { get; set; }


        // Propiedad de navegación para PlanPersonalizado
        public ICollection<PlanPersonalizado> PlanesPersonalizados { get; set; }

        // Servicios adicionales contratados
        public ICollection<ServicioAdicional> ServiciosAdicionales { get; set; }

        // Propiedad de navegación para los pagos realizados
        public ICollection<Pago> Pagos { get; set; }
    }
}
