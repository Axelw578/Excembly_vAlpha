namespace Excembly_vAlpha.ViewModels
{
    public class CitaViewModel
    {
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int PlanId { get; set; }
        public int TecnicoId { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string Estado { get; set; }
        public string Comentarios { get; set; }
    }
}
