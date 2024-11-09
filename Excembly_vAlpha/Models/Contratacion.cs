﻿using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.Models
{
    // actualizado
    public class Contratacion
    {
        public int ContratacionId { get; set; }
        public int UsuarioId { get; set; }
        public int? PlanId { get; set; }
        public int? ServicioId { get; set; }
        public int? PlanPersonalizadoId { get; set; }  // Referencia a PlanPersonalizado
        public int? CitaId { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;

        // Estado de la contratación: Activa, Cancelada, Completada
        public string Estado { get; set; } = "Activa";

        // Fecha de cancelación
        public DateTime? FechaCancelacion { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Plan Plan { get; set; }
        public Servicio Servicio { get; set; }
        public PlanPersonalizado PlanPersonalizado { get; set; }
        public Cita Cita { get; set; }
        public ICollection<ServicioAdicionalContratado> ServiciosAdicionalesContratados { get; set; }
    }
}