using System;

namespace Excembly_vAlpha.ViewModels
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
