namespace Excembly_vAlpha.ViewModels
{
public class ContratacionesIndexViewModel
{
    public List<ContratacionIndexItem> Contrataciones { get; set; } = new List<ContratacionIndexItem>();
}

public class ContratacionIndexItem
{
    public int ContratacionId { get; set; }
    public string Estado { get; set; }
    public string PlanNombre { get; set; }
    public string ServicioNombre { get; set; }
    public DateTime FechaContratacion { get; set; }
    public decimal PrecioTotal { get; set; }
    public string TecnicoNombre { get; set; }
}

}
