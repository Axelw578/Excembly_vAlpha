using AutoMapper;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using System.Linq;

namespace Excembly_vAlpha.Mapping
{
    public class ContratacionProfile : Profile
    {
        public ContratacionProfile()
        {
            // Mapeo de Contratacion a ContratacionViewModel
            CreateMap<Contratacion, ContratacionViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre + " " + src.Usuario.Apellidos))
                .ForMember(dest => dest.CorreoUsuario, opt => opt.MapFrom(src => src.Usuario.CorreoElectronico))
                .ForMember(dest => dest.NombrePlan, opt => opt.MapFrom(src => src.Plan.Nombre))
                .ForMember(dest => dest.PrecioPlan, opt => opt.MapFrom(src => src.Plan.Precio))
                .ForMember(dest => dest.NombreServicio, opt => opt.MapFrom(src => src.Servicio.Nombre))
                .ForMember(dest => dest.PrecioServicio, opt => opt.MapFrom(src => src.Servicio.Precio))
                .ForMember(dest => dest.ServiciosAdicionalesContratados, opt => opt.MapFrom(src => src.ServiciosAdicionalesContratados))
                .ForMember(dest => dest.PlanesDisponibles, opt => opt.Ignore()) // Ignorados en la creación
                .ForMember(dest => dest.ServiciosDisponibles, opt => opt.Ignore()) // Ignorados en la creación
                .ForMember(dest => dest.ServiciosAdicionalesDisponibles, opt => opt.Ignore()) // Ignorados en la creación
                .ReverseMap()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore()) // Usuario se asigna manualmente
                .ForMember(dest => dest.Plan, opt => opt.Ignore()) // Plan se asigna manualmente
                .ForMember(dest => dest.Servicio, opt => opt.Ignore()) // Servicio se asigna manualmente
                .ForMember(dest => dest.ServiciosAdicionalesContratados, opt => opt.Ignore()); // Asignados manualmente

            // Mapeo de Servicio a ServicioViewModel
            CreateMap<Servicio, ServicioViewModel>()
                .ForMember(dest => dest.ImagenUrl, opt => opt.MapFrom(src => src.Imagen));

            // Mapeo de Plan a PlanViewModel
            CreateMap<Plan, PlanViewModel>()
                .ForMember(dest => dest.ServiciosIncluidos, opt => opt.MapFrom(src => src.PlanServicios.Select(ps => ps.Servicio.Nombre)))
                .ForMember(dest => dest.ServiciosAdicionales, opt => opt.MapFrom(src => src.ServiciosAdicionales));

            // Mapeo de ServicioAdicional a ServicioAdicionalViewModel
            CreateMap<ServicioAdicional, ServicioAdicionalViewModel>()
                .ForMember(dest => dest.NombreServicio, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.PrecioOriginal, opt => opt.MapFrom(src => src.Servicio.Precio))
                .ForMember(dest => dest.PrecioConDescuento, opt => opt.MapFrom(src => src.Servicio.Precio * (1 - src.Descuento)))
                .ForMember(dest => dest.Descuento, opt => opt.MapFrom(src => src.Descuento));

            // Mapeo de ServicioAdicionalContratado a ServicioAdicionalViewModel
            CreateMap<ServicioAdicionalContratado, ServicioAdicionalViewModel>()
                .ForMember(dest => dest.ServicioId, opt => opt.MapFrom(src => src.ServicioAdicional.ServicioId))
                .ForMember(dest => dest.NombreServicio, opt => opt.MapFrom(src => src.ServicioAdicional.Nombre))
                .ForMember(dest => dest.PrecioOriginal, opt => opt.MapFrom(src => src.ServicioAdicional.Servicio.Precio))
                .ForMember(dest => dest.PrecioConDescuento, opt => opt.MapFrom(src => src.ServicioAdicional.Servicio.Precio * (1 - src.DescuentoAplicado)))
                .ForMember(dest => dest.Descuento, opt => opt.MapFrom(src => src.DescuentoAplicado));

            // **Nuevo mapeo: ServicioAdicionalContratado a ServicioAdicionalContratadoViewModel**
            CreateMap<ServicioAdicionalContratado, ServicioAdicionalContratadoViewModel>()
                .ForMember(dest => dest.ServicioAdicionalNombre, opt => opt.MapFrom(src => src.ServicioAdicional.Nombre))
                .ForMember(dest => dest.ServicioAdicionalPrecio, opt => opt.MapFrom(src => src.ServicioAdicional.Servicio.Precio));

            // **Nuevo mapeo: ServicioContratado a ServicioContratadoViewModel**
            CreateMap<ServicioContratado, ServicioContratadoViewModel>()
                .ForMember(dest => dest.NombreServicio, opt => opt.MapFrom(src => src.Servicio.Nombre))
                .ForMember(dest => dest.PrecioServicio, opt => opt.MapFrom(src => src.Servicio.Precio));
        }
    }
}
