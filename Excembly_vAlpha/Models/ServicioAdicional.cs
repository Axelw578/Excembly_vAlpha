﻿namespace Excembly_vAlpha.Models
{
    // actualizado
    public class ServicioAdicional
    {
        public int Id { get; set; }  // ID propio para identificador único
        public int PlanId { get; set; }
        public int ServicioId { get; set; }
        public decimal Descuento { get; set; } = 0.10m;

        // Relaciones
        public Plan Plan { get; set; }
        public Servicio Servicio { get; set; }
    }
}
