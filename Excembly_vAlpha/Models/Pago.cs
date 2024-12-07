using System;

namespace Excembly_vAlpha.Models
{
    // actualizado
    public class Pago
    {
        public int PagoId { get; set; }
        public int UsuarioId { get; set; }
        public int? ContratacionId { get; set; }
        public int? ServicioId { get; set; }  // Contratación individual de servicio
        public int? PlanId { get; set; }  // Pago directo de un plan
        public int? TarjetaId { get; set; }  // Tarjeta guardada utilizada en el pago
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
        public int? CitaId { get; set; } // Si la referencia a Cita puede ser nula

        // Relaciones
        public Cita Cita { get; set; }
        public Usuario Usuario { get; set; }
        public Contratacion Contratacion { get; set; }
        public TarjetaGuardada TarjetaGuardada { get; set; } // Relación con TarjetaGuardada
    }

}
