using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class DireccionViewModel
    {
        public int UsuarioId { get; set; } // ID del usuario asociado

        public int? DireccionId { get; set; } // ID de la dirección si existe

        [Required(ErrorMessage = "La colonia es obligatoria.")]
        public string Colonia { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria.")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El número de edificio es obligatorio.")]
        public string NumeroEdificio { get; set; }

        public string DescripcionEdificio { get; set; }
        public string ReferenciaEdificio { get; set; }

        public bool TieneDireccion { get; set; } // Propiedad para control interno
    }
}
