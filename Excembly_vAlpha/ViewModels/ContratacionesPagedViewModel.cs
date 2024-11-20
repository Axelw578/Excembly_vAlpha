namespace Excembly_vAlpha.ViewModels
{
    public class ContratacionesPagedViewModel
    {
        public List<ContratacionViewModel> Contrataciones { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool NoContrataciones { get; set; } // Indicador si no hay contrataciones
    }

}
