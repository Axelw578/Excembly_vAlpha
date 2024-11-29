namespace Excembly_vAlpha.ViewModels
{
    public class AsignarTecnicoViewModel
    {
        public int ContratacionId { get; set; }
        public int TecnicoId { get; set; } // Nueva propiedad
        public List<TecnicoViewModel> Tecnicos { get; set; }
    }
}
