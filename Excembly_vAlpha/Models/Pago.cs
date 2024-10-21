using System;

namespace Excembly_vAlpha.Models
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int UsuarioId { get; set; }
        public int? CitaId { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = "tarjeta"; // Default
        public string Estado { get; set; } = "pendiente"; // Default
        public string Referencia { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Cita Cita { get; set; }
    }
}
