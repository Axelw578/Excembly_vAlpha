using System;

namespace Excembly_vAlpha.Models
{
    public class DispositivoPlanFamiliar
    {
        public int DispositivoId { get; set; }
        public int UsuarioId { get; set; }
        public string MACAddress { get; set; }
        public int PlanId { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool EquipoAdicional { get; set; } = false;

        public Usuario Usuario { get; set; }
        public Plan Plan { get; set; }
    }

}
