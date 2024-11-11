using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class DireccionViewModel
    {
        // Propiedades del Usuario
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string FotoPerfilUrl { get; set; }

        // Propiedades de la Dirección
        public int? DireccionId { get; set; }

        [Required(ErrorMessage = "La colonia es obligatoria.")]
        public string Colonia { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria.")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El número de edificio es obligatorio.")]
        public string NumeroEdificio { get; set; }

        public string DescripcionEdificio { get; set; }
        public string ReferenciaEdificio { get; set; }

        // Propiedad para indicar si la dirección ya está registrada
        public bool TieneDireccion { get; set; }
    }
}
