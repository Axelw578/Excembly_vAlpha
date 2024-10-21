namespace Excembly_vAlpha.Models
{
    public class PoliticaPrivacidad
    {
        public int PoliticaPrivacidadId { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
