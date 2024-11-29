using AutoMapper;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.MappingProfiles
{
    public class ContratacionAdminProfile : Profile
    {
        public ContratacionAdminProfile()
        {
            // Mapeo para ContratacionAdminViewModel
            CreateMap<Contratacion, ContratacionAdminViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.ApellidoUsuario, opt => opt.MapFrom(src => src.Usuario.Apellidos))
                .ForMember(dest => dest.CorreoUsuario, opt => opt.MapFrom(src => src.Usuario.CorreoElectronico))
                .ForMember(dest => dest.TelefonoUsuario, opt => opt.MapFrom(src => src.Usuario.Telefono))
                .ForMember(dest => dest.PlanContratado, opt => opt.MapFrom(src => src.Plan != null ? src.Plan.Nombre : null))
                .ForMember(dest => dest.ServicioContratado, opt => opt.MapFrom(src => src.Servicio != null ? src.Servicio.Nombre : null));

            // Mapeo para PagoAdminViewModel
            CreateMap<Pago, PagoAdminViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.CorreoUsuario, opt => opt.MapFrom(src => src.Usuario.CorreoElectronico));

            // Mapeo para AsignacionTecnicoAdminViewModel
            CreateMap<AsignacionTecnico, AsignacionTecnicoAdminViewModel>()
                .ForMember(dest => dest.NombreTecnico, opt => opt.MapFrom(src => src.Tecnico.Nombre))
                .ForMember(dest => dest.ApellidoTecnico, opt => opt.MapFrom(src => src.Tecnico.Apellidos))
                .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.Contratacion.FechaContratacion))
                .ForMember(dest => dest.EstadoContratacion, opt => opt.MapFrom(src => src.Contratacion.Estado));

            CreateMap<Tecnico, TecnicoViewModel>()
    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
    .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
    .ForMember(dest => dest.Disponibilidad, opt => opt.MapFrom(src => src.Disponibilidad));



        }
    }
}
