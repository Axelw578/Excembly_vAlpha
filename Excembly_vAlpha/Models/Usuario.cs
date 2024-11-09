using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.Models
{
    // actualizado
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public int? DireccionId { get; set; }
        public string Telefono { get; set; }
        public int? RolId { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string FotoPerfilUrl { get; set; }

        public Direccion Direccion { get; set; }
        public Rol Rol { get; set; }

        // Colecciones de navegación
        public ICollection<Cita> Citas { get; set; }
        public ICollection<Pago> Pagos { get; set; }
        public ICollection<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; }
        public ICollection<TarjetaGuardada> TarjetasGuardadas { get; set; }
        public ICollection<BitacoraEvento> BitacoraEventos { get; set; }
        public ICollection<PlanPersonalizado> PlanesPersonalizados { get; set; }
        public ICollection<AsignacionTecnico> AsignacionesTecnico { get; set; }
        public ICollection<Trabajo> Trabajos { get; set; }

        // Nueva propiedad para la relación con Contratacion
        public ICollection<Contratacion> Contrataciones { get; set; } = new List<Contratacion>();
    }
}
