using System.Collections.Generic;

namespace Excembly_vAlpha.ViewModels
{
    public class AsignarTecnicoViewModel
    {
        public int ContratacionId { get; set; }
        public int TecnicoId { get; set; }

        // Inicializa la lista para evitar errores
        public List<TecnicoViewModel> Tecnicos { get; set; } = new List<TecnicoViewModel>();
    }

}
