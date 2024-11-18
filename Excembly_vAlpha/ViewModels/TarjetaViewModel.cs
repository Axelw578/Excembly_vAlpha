using System;
using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class TarjetaViewModel
    {
        public int? TarjetaId { get; set; } // Para identificar la tarjeta seleccionada (si existe)

        [Required(ErrorMessage = "El nombre del titular es requerido")]
        public string NombreTitular { get; set; }

        [Required(ErrorMessage = "El número de tarjeta es requerido")]
        [CreditCard(ErrorMessage = "Debe ingresar un número de tarjeta válido")]
        public string NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "El mes de vencimiento es requerido")]
        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12")]
        public int MesExpiracion { get; set; }

        [Required(ErrorMessage = "El año de vencimiento es requerido")]
        [Range(2024, 2035, ErrorMessage = "El año debe estar entre 2024 y 2035")]
        public int AñoExpiracion { get; set; }

        [Required(ErrorMessage = "El CVV es requerido")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El CVV debe tener exactamente 3 dígitos")]
        public string CVV { get; set; }

        public string Banco { get; set; } // Opcional
        public string Marca { get; set; } // Ejemplo: Visa, MasterCard

        [Required(ErrorMessage = "El tipo de tarjeta es requerido")]
        public string TipoTarjeta { get; set; } // Débito o Crédito

        public bool EsTarjetaSeleccionada { get; set; } // Indica si la tarjeta está seleccionada para un pago actual

        public bool EsEdicion { get; set; } // NUEVA PROPIEDAD: Identifica si es edición o creación
    }
}
