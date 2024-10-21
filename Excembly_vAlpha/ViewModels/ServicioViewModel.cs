namespace Excembly_vAlpha.ViewModels
{
    public class ServicioViewModel
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Duracion { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
        public string TipoServicio { get; set; }
        public bool EsExclusivo { get; set; }
    }
}
