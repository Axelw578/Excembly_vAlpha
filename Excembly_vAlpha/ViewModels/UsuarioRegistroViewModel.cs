using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class UsuarioRegistroViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden tener más de 100 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres.")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido.")]
        public string Telefono { get; set; }

        [Url(ErrorMessage = "Ingrese una URL válida para la foto de perfil.")]
        public string FotoPerfilUrl { get; set; }

        // Nueva propiedad para la opción de registrar dirección
        public bool RegistrarDireccion { get; set; }
    }
}
