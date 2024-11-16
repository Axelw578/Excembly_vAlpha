namespace Excembly_vAlpha.ViewModels
{
    public class CancelarContratacionViewModel
    {
        public int ContratacionId { get; set; }
        public string PoliticasCancelacion { get; set; }
        public string MensajeConfirmacion { get; set; } = "¿Está seguro de que desea cancelar esta contratación?";
    }
}
