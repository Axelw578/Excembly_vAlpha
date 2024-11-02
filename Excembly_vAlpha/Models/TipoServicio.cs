namespace Excembly_vAlpha.Models
{
    public class TipoServicio
    {
        public int TipoServicioId { get; set; }
        public string NombreTipoServicio { get; set; }

        public ICollection<Servicio> Servicios { get; set; }
    }

}
