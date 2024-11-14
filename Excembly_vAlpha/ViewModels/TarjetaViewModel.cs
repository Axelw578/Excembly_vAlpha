using System;
using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class TarjetaViewModel
    {
        public int? TarjetaId { get; set; }  // Para identificar la tarjeta seleccionada (si existe)

        [Required(ErrorMessage = "El nombre del titular es requerido")]
        public string NombreTitular { get; set; }

        [Required(ErrorMessage = "El número de tarjeta es requerido")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "El número de tarjeta debe tener 16 dígitos")]
        public string NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "El mes de vencimiento es requerido")]
        public int MesExpiracion { get; set; }

        [Required(ErrorMessage = "El año de vencimiento es requerido")]
        public int AñoExpiracion { get; set; }

        [Required(ErrorMessage = "El CVV es requerido")]
        [StringLength(3, ErrorMessage = "El CVV debe tener 3 dígitos")]
        public string CVV { get; set; }

        public string Banco { get; set; }   // Opcional, dependiendo si se quiere mostrar el banco de la tarjeta
        public string Marca { get; set; }   // Ejemplo: Visa, MasterCard

        [Required(ErrorMessage = "El tipo de tarjeta es requerido")]
        public string TipoTarjeta { get; set; }  // Debito o Credito 'nuevo'

        public bool EsTarjetaSeleccionada { get; set; }  // Indica si la tarjeta está seleccionada para un pago actual
    }
}
