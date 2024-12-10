namespace Excembly_vAlpha.Models
{
    public class ServicioAdicionalContratado
    {
        public int Id { get; set; }

        // Clave foránea hacia Contratacion
        public int ContratacionId { get; set; }

        // Clave foránea compuesta hacia ServicioAdicional
        public int ServicioAdicionalId { get; set; }
        public int PlanId { get; set; } // Parte de la clave compuesta

        public decimal DescuentoAplicado { get; set; }

        // Relaciones de navegación
        public Contratacion Contratacion { get; set; }
        public ServicioAdicionales ServicioAdicional { get; set; }
    }
}
