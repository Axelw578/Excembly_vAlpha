using System.ComponentModel.DataAnnotations;

namespace Excembly_vAlpha.ViewModels
{
    public class EditarTarjetaViewModel
    {
        public int TarjetaId { get; set; }
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public int MesExpiracion { get; set; }
        public int AñoExpiracion { get; set; }
        public string CVV { get; set; }
        public string Banco { get; set; }
        public string Marca { get; set; }
        public string TipoTarjeta { get; set; }
    }



}
