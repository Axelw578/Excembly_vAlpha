namespace Excembly_vAlpha.ViewModels
{
    public class AsignacionTecnicoViewModel
    {
        public int AsignacionId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public int ServicioId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string Estado { get; set; }
    }
}
