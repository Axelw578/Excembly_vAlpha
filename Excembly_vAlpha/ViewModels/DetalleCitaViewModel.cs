using Excembly_vAlpha.Models;
using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.ViewModels
{
    public class DetalleCitaViewModel
    {
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public int? DireccionId { get; set; }
        public Direccion Direccion { get; set; }
        public int? PlanId { get; set; }
        public Plan Plan { get; set; }
        public ICollection<ServicioAdicional> ServiciosAdicionales { get; set; }
        public DateTime FechaCita { get; set; }
        public DateTime? FechaCitaModificada { get; set; }
        public string EstadoCita { get; set; }
        public bool Domicilio { get; set; }
        public string Comentarios { get; set; }
        public string Imagen { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string MotivoCancelacion { get; set; }

        // Contratación asociada
        public Contratacion Contratacion { get; set; }

        // Propiedad calculada en el ViewModel
        public string DescripcionDireccion => Direccion != null
            ? $"{Direccion.Calle} {Direccion.NumeroEdificio}, {Direccion.Colonia}. {Direccion.DescripcionEdificio}. Ref: {Direccion.ReferenciaEdificio}"
            : "No especificada";
    }
}
