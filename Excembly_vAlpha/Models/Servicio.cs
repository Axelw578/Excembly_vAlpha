namespace Excembly_vAlpha.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Duracion { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
        public string TipoServicio { get; set; } = "preventivo"; // Default
        public bool EsExclusivo { get; set; } = false;

        // Relacin con Plan
        public ICollection<Plan_Servicio> Planes { get; set; }
        public ICollection<AsignacionTecnico> AsignacionTecnicos { get; set; }
    }
}
