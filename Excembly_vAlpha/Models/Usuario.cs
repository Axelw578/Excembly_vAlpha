using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; } = "cliente"; // Default 'cliente'
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con otras entidades
        public ICollection<Trabajo> Trabajos { get; set; }
        public ICollection<Cita> Citas { get; set; }
        public ICollection<Pago> Pagos { get; set; }
        public ICollection<HistorialUsoPlan> HistorialUsoPlanes { get; set; }
        public ICollection<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; }
        public ICollection<TarjetaGuardada> TarjetasGuardadas { get; set; }
        public ICollection<BitacoraEvento> BitacoraEventos { get; set; }
        public ICollection<AsignacionTecnico> AsignacionTecnicos { get; set; }
        public ICollection<CambioContratacion> CambiosContratacion { get; set; }
    }
}
