using AutoMapper;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Mapping
{
    public class ComentariosAdminProfile : Profile
    {
        public ComentariosAdminProfile()
        {
            // Mapeo de Comentario a ComentarioAdminViewModel
            CreateMap<Comentario, ComentarioAdminViewModel>()
                .ForMember(dest => dest.ComentarioId, opt => opt.MapFrom(src => src.ComentarioId))
                .ForMember(dest => dest.ComentarioTexto, opt => opt.MapFrom(src => src.Opinion))
                .ForMember(dest => dest.FechaComentario, opt => opt.MapFrom(src => src.FechaComentario))
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.ContratacionId, opt => opt.MapFrom(src => src.ContratacionId))
                .ForMember(dest => dest.PlanNombre,
                           opt => opt.MapFrom(src => src.Contratacion.Plan != null ? src.Contratacion.Plan.Nombre : "No Disponible"))
                .ForMember(dest => dest.ServicioNombre,
                           opt => opt.MapFrom(src => src.Contratacion.Servicio != null ? src.Contratacion.Servicio.Nombre : "No Disponible"))
                .ForMember(dest => dest.TecnicoNombre,
                           opt => opt.MapFrom(src => src.Contratacion.AsignacionesTecnico.FirstOrDefault() != null
                                                     ? src.Contratacion.AsignacionesTecnico.FirstOrDefault().Tecnico.Nombre
                                                     : "Sin Asignar"))
                .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.Contratacion.FechaContratacion));

            // Mapeo de Comentario a ComentarioDetalleAdminViewModel
            CreateMap<Comentario, ComentarioDetalleAdminViewModel>()
                .ForMember(dest => dest.ComentarioId, opt => opt.MapFrom(src => src.ComentarioId))
                .ForMember(dest => dest.ComentarioTexto, opt => opt.MapFrom(src => src.Opinion))
                .ForMember(dest => dest.FechaComentario, opt => opt.MapFrom(src => src.FechaComentario))
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.TecnicoNombre,
                           opt => opt.MapFrom(src => src.Contratacion.AsignacionesTecnico.FirstOrDefault() != null
                                                     ? src.Contratacion.AsignacionesTecnico.FirstOrDefault().Tecnico.Nombre
                                                     : "Sin Asignar"))
                .ForMember(dest => dest.PlanNombre,
                           opt => opt.MapFrom(src => src.Contratacion.Plan != null ? src.Contratacion.Plan.Nombre : "No Disponible"))
                .ForMember(dest => dest.ServicioNombre,
                           opt => opt.MapFrom(src => src.Contratacion.Servicio != null ? src.Contratacion.Servicio.Nombre : "No Disponible"))
                .ForMember(dest => dest.FechaContratacion, opt => opt.MapFrom(src => src.Contratacion.FechaContratacion))
                .ForMember(dest => dest.TecnicoDetalles,

                           opt => opt.MapFrom(src => src.Contratacion.AsignacionesTecnico.FirstOrDefault() != null
                                                     ? src.Contratacion.AsignacionesTecnico.FirstOrDefault().Tecnico.Experiencia
                                                     : "No Disponible"))
                .ForMember(dest => dest.DetallesContratacion,
                           opt => opt.MapFrom(src => src.Contratacion.TipoServicio ?? "No Disponible"));
        }
    }

}
