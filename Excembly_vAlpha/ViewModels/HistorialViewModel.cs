namespace Excembly_vAlpha.ViewModels
{
    public class HistorialViewModel
    {
        public int HistorialId { get; set; }
        public int PlanId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaUso { get; set; }
        public string Descripcion { get; set; }
        public string CodigoServicio { get; set; }
        public bool Usado { get; set; }
    }
}
