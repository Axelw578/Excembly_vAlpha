namespace Excembly_vAlpha.Models
{
    public class Direccion
    {
        public int DireccionId { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumeroEdificio { get; set; }
        public string DescripcionEdificio { get; set; }
        public string ReferenciaEdificio { get; set; }
       
        // Nueva propiedad calculada
        public string Descripcion =>
            $"{Calle} {NumeroEdificio}, {Colonia}. {DescripcionEdificio}. Ref: {ReferenciaEdificio}";


    }

}
