namespace Excembly_vAlpha.Models
{
    // nuevo
    public class ServicioAdicionalContratado
    {
        public int Id { get; set; }
        public int ContratacionId { get; set; }
        public int ServicioAdicionalId { get; set; }
        public decimal DescuentoAplicado { get; set; }

        public Contratacion Contratacion { get; set; }
        public ServicioAdicional ServicioAdicional { get; set; }
    }
}
