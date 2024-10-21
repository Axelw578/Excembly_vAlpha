using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.Models
{
    public class Tecnico
    {
        public int TecnicoId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Experiencia { get; set; }
        public string Foto { get; set; }
        public string Disponibilidad { get; set; } = "disponible"; // Default
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con otras entidades
        public ICollection<Trabajo> Trabajos { get; set; }
        public ICollection<Cita> Citas { get; set; }
        public ICollection<AsignacionTecnico> AsignacionTecnicos { get; set; }
    }
}
