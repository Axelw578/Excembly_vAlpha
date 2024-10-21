using System;

namespace Excembly_vAlpha.Models
{
    public class TarjetaGuardada
    {
        public int TarjetaId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreTarjetaHabiente { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string CVV { get; set; }
        public string TipoTarjeta { get; set; } = "Visa"; // Default

        // Relaciones
        public Usuario Usuario { get; set; }
    }
}
