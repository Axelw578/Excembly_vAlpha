using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class EditarTarjetaViewModel
    {
        public int TarjetaId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre del titular no debe exceder 50 caracteres.")]
        public string NombreTitular { get; set; }

        [Range(1, 12, ErrorMessage = "El mes de expiración debe estar entre 1 y 12.")]
        public int MesExpiracion { get; set; }

        [Range(2024, 2100, ErrorMessage = "El año de expiración debe ser válido.")]
        public int AñoExpiracion { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El CVV debe tener 3 dígitos.")]
        public string CVV { get; set; }
    }


}
