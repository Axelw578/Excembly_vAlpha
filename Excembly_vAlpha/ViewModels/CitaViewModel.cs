namespace Excembly_vAlpha.ViewModels
{
    public class CitaViewModel
    {
        public int CitaId { get; set; }
        public int UsuarioId { get; set; }
        public int? DireccionId { get; set; }
        public string Direccion { get; set; } // Puedes adaptarlo si tienes una relación con Direccion
        public DateTime FechaCita { get; set; }
        public string EstadoCita { get; set; }
        public string Comentarios { get; set; }
    }
}
