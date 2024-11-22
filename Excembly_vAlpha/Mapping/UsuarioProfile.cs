using AutoMapper;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            // Mapeo de Usuario a UsuarioViewModel
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}
