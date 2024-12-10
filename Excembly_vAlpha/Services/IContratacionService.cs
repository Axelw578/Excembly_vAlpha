using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public interface IContratacionService
    {
        Task<IEnumerable<Contratacion>> ObtenerContratacionesDelUsuario(int usuarioId);
        Task<Contratacion?> ObtenerContratacionPorId(int contratacionId);
        Task<bool> AgregarContratacion(Contratacion contratacion);
        Task<bool> EditarContratacion(Contratacion contratacion);
        Task<bool> CancelarContratacion(int contratacionId);
        Task<bool> SeleccionarPlanOServicio(int contratacionId, int? planId, List<int>? serviciosIds, List<int>? serviciosAdicionalesIds);

        Task<IEnumerable<Plan>> ObtenerPlanesDisponibles();
        Task<Plan> ObtenerPlanPorId(int planId);
        Task<IEnumerable<Servicio>> ObtenerServiciosDisponibles();
        Task<Servicio> ObtenerServicioPorId(int servicioId);
        Task<IEnumerable<ServicioAdicionales>> ObtenerServiciosAdicionalesDisponibles();

        Task<ServicioAdicionales> ObtenerServicioAdicionalPorId(int servicioAdicionalId, int? planId = null);

        Task<bool> AgregarServicioAdicionalContratado(ServicioAdicionalContratado servicioAdicionalContratado);

        Task<ContratacionViewModel?> ObtenerDetallesDeContratacion(int contratacionId);

        // Nuevo método
        Task<IEnumerable<TarjetaGuardada>> ObtenerTarjetasGuardadasDelUsuario(int usuarioId);
         Task<bool> RegistrarPago(Pago pago);

        Task<bool> AgregarServicioContratado(ServicioContratado servicioContratado);


        Task<IEnumerable<ServicioAdicionalContratadoViewModel>> ObtenerServiciosAdicionalesContratadosPorContratacionId(int contratacionId);



    }
}
