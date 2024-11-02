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
        public bool Disponibilidad { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Cita> Citas { get; set; }
        public ICollection<AsignacionTecnico> AsignacionesTecnico { get; set; }
        public ICollection<Trabajo> Trabajos { get; set; }
    }

}
