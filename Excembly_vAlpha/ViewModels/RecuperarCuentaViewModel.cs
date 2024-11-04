using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class RecuperarCuentaViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Introduce una dirección de correo válida.")]
        public string Correo { get; set; }
    }
}
