using System.Collections.Generic;

namespace Excembly_vAlpha.ViewModels
{
    public class EmpresaViewModel
    {
        public string MisionVision { get; set; }
        public string HorarioTienda { get; set; }
        public string DomicilioTienda { get; set; }
        public string Aclaracion { get; set; }
        public string PoliticaCancelacion { get; set; }
        public string ImagenMapa { get; set; }

        // Agregar la propiedad Reseñas como una lista de ComentarioViewModel
        public List<ComentarioViewModel> Reseñas { get; set; }
    }
}
