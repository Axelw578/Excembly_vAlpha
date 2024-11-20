using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectListItem
using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.ViewModels
{
    public class CrearCitaViewModel
    {
        public int CitaId { get; set; }  // Agregado CitaId
        public int UsuarioId { get; set; }
        public int? DireccionId { get; set; }
        public int? TecnicoId { get; set; }
        public int? PlanId { get; set; }
        public List<int> ServiciosAdicionales { get; set; } = new List<int>();
        public DateTime FechaCita { get; set; }
        public bool Domicilio { get; set; }
        public string Comentarios { get; set; }

        // Nueva propiedad para las contrataciones disponibles
        public List<SelectListItem> ContratacionesDisponibles { get; set; } = new List<SelectListItem>();
    }
}
