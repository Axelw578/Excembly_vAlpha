﻿namespace Excembly_vAlpha.ViewModels
{
    public class PlanViewModel
    {
        public int PlanId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }

        // Lista de nombres de servicios incluidos en el plan
        public List<string> ServiciosIncluidos { get; set; }

        // Lista de servicios adicionales con sus descuentos aplicados
        public List<ServicioAdicionalViewModel> ServiciosAdicionales { get; set; }
    }


}
