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
               .ForMember(dest => dest.PlanContratado, opt => opt.MapFrom(src => src.Plan != null ? src.Plan.Nombre : "No especificado"))
.ForMember(dest => dest.ServicioContratado, opt => opt.MapFrom(src => src.Servicio != null ? src.Servicio.Nombre : "No especificado"))
                .ForMember(dest => dest.ServicioContratado, opt => opt.MapFrom(src => src.Servicio != null ? src.Servicio.Nombre : null))
                .ForMember(dest => dest.ServiciosAdicionalesContratados, opt => opt.MapFrom(src =>
                    src.ServiciosAdicionalesContratados.Select(sac => sac.ServicioAdicional.Nombre).ToList()));


            // Mapeo entre ContratacionAdminViewModelDTO y ContratacionAdminViewModel
            CreateMap<ContratacionAdminViewModelDTO, ContratacionAdminViewModel>()
                .ForMember(dest => dest.ContratacionId, opt => opt.MapFrom(src => src.ContratacionId))
                .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.FechaContratacion))
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.PlanContratado, opt => opt.MapFrom(src => src.Plan.Nombre))
                .ForMember(dest => dest.ServiciosAdicionalesContratados, opt => opt.MapFrom(src =>
                    src.ServiciosAdicionales.Select(sa => sa.Nombre).ToList()));

            // Mapeo de un modelo específico (en lugar de 'SomeSpecificModel', usa el tipo real)
            CreateMap<Contratacion, ContratacionAdminViewModelDTO>()
                .ForMember(dest => dest.ContratacionId, opt => opt.MapFrom(src => src.ContratacionId))
                .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.FechaContratacion))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => new UsuarioDTO
                {
                    UsuarioId = src.Usuario.UsuarioId,
                    Nombre = src.Usuario.Nombre
                }))
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => new PlanDTO
                {
                    PlanId = src.Plan.PlanId,
                    Nombre = src.Plan.Nombre
                }))
                .ForMember(dest => dest.ServiciosAdicionales, opt => opt.MapFrom(src =>
                    src.ServiciosAdicionalesContratados.Select(sa => new ServicioAdicionalDTO
                    {
                        ServicioAdicionalId = sa.ServicioAdicionalId,
                        Nombre = sa.ServicioAdicional.Nombre
                    }).ToList()));

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

            // Mapeo para TecnicoViewModel
            CreateMap<Tecnico, TecnicoViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Disponibilidad, opt => opt.MapFrom(src => src.Disponibilidad));

            // Mapeo para DetalleContratacionAdminViewModel
            CreateMap<Contratacion, DetalleContratacionAdminViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.CorreoUsuario, opt => opt.MapFrom(src => src.Usuario.CorreoElectronico))
                .ForMember(dest => dest.PlanContratado, opt => opt.MapFrom(src => src.Plan != null ? src.Plan.Nombre : null))
                .ForMember(dest => dest.ServicioContratado, opt => opt.MapFrom(src => src.Servicio != null ? src.Servicio.Nombre : null))
                .ForMember(dest => dest.ServiciosAdicionalesContratados, opt => opt.MapFrom(src =>
                    src.ServiciosAdicionalesContratados.Select(sac => sac.ServicioAdicional.Nombre).ToList()))
                .ForMember(dest => dest.Pagos, opt => opt.Ignore()); // Los pagos se asignan manualmente

            // Mapeo entre ContratacionAdminViewModel y DetalleContratacionAdminViewModel
            CreateMap<ContratacionAdminViewModel, DetalleContratacionAdminViewModel>()
                .ForMember(dest => dest.Pagos, opt => opt.Ignore()); // Pagos se deben asignar manualmente

            // Mapeo para TarjetaGuardada -> TarjetaViewModel
            CreateMap<TarjetaGuardada, TarjetaViewModel>()
                .ForMember(dest => dest.TarjetaId, opt => opt.MapFrom(src => src.TarjetaId))
                .ForMember(dest => dest.NombreTitular, opt => opt.MapFrom(src => src.NombreTitular))
                .ForMember(dest => dest.NumeroTarjeta, opt => opt.MapFrom(src => src.NumeroTarjeta))
                .ForMember(dest => dest.MesExpiracion, opt => opt.MapFrom(src => src.FechaExpiracion.Month))
                .ForMember(dest => dest.AñoExpiracion, opt => opt.MapFrom(src => src.FechaExpiracion.Year))
                .ForMember(dest => dest.CVV, opt => opt.MapFrom(src => src.CVV))
                .ForMember(dest => dest.TipoTarjeta, opt => opt.MapFrom(src => src.TipoTarjeta))
                .ForMember(dest => dest.Banco, opt => opt.MapFrom(src => src.Banco))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca));


        }
    }
}
