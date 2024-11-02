namespace Excembly_vAlpha.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string TipoRol { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }

}
