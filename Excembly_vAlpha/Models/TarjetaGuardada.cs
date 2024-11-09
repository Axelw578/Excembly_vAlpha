using System;

namespace Excembly_vAlpha.Models
{
    // actualizado
    public class TarjetaGuardada
    {
        public int TarjetaId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string CVV { get; set; }
        public string TipoTarjeta { get; set; }
        public string Banco { get; set; }
        public string Marca { get; set; }  // Ej. Visa, MasterCard

        // Relación con Usuario
        public Usuario Usuario { get; set; }
    }
}
