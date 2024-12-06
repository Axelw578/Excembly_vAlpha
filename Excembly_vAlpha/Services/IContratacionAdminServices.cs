using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Services
{
    public interface IContratacionAdminServices
    {
        Task<IEnumerable<ContratacionAdminViewModel>> ObtenerTodasContratacionesAsync();
        Task<bool> MarcarComoCompletadoAsync(int contratacionId);
        Task<IEnumerable<ContratacionAdminViewModel>> FiltrarContratacionesAsync(DateTime? fechaInicio, DateTime? fechaFin, int? usuarioId = null);
        Task<ContratacionAdminViewModel> ObtenerDetalleContratacionAsync(int contratacionId);
        Task<IEnumerable<PagoAdminViewModel>> ObtenerPagosPorContratacionAsync(int contratacionId);
        Task<bool> AsignarTecnicoAsync(int contratacionId, int tecnicoId);
        Task<IEnumerable<Tecnico>> ObtenerTecnicosAsync();

    }
}
