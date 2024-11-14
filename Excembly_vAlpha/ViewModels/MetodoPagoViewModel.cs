using System.Collections.Generic;
using System.Linq;

namespace Excembly_vAlpha.ViewModels
{
    public class MetodoPagoViewModel
    {
        public string NombreUsuario { get; set; }
        public List<TarjetaViewModel> TarjetasGuardadas { get; set; }
        public TarjetaViewModel TarjetaSeleccionada { get; set; }
        public PoliticaViewModel Politica { get; set; }

        // verifica si hay tarjetas guardadas
        public bool TieneTarjetaGuardada => TarjetasGuardadas != null && TarjetasGuardadas.Any();
    }
}
