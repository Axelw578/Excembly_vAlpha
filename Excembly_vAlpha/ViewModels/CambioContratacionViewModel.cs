namespace Excembly_vAlpha.ViewModels
{
    public class CambioContratacionViewModel
    {
        public int CambioId { get; set; }
        public int UsuarioId { get; set; }
        public int PlanId { get; set; }
        public DateTime FechaCambio { get; set; }
        public string TipoCambio { get; set; }
        public string Comentarios { get; set; }
    }
}
