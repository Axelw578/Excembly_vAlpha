﻿using Excembly_vAlpha.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public interface IContratacionService
    {
        Task<IEnumerable<Contratacion>> ObtenerContratacionesDelUsuario(int usuarioId);
        Task<Contratacion?> ObtenerContratacionPorId(int contratacionId);
        Task<bool> AgregarContratacion(Contratacion contratacion);
        Task<bool> EditarContratacion(Contratacion contratacion); // Renombrado para coincidir con el controlador
        Task<bool> CancelarContratacion(int contratacionId);
        Task<bool> SeleccionarPlanOServicio(int contratacionId, int? planId, int? servicioId, List<int>? serviciosAdicionalesIds);

        Task<IEnumerable<Plan>> ObtenerPlanesDisponibles();
        Task<Plan> ObtenerPlanPorId(int planId);
        Task<IEnumerable<Servicio>> ObtenerServiciosDisponibles();
        Task<Servicio> ObtenerServicioPorId(int servicioId);
        Task<IEnumerable<ServicioAdicional>> ObtenerServiciosAdicionalesDisponibles();
        Task<ServicioAdicional> ObtenerServicioAdicionalPorId(int servicioAdicionalId);

        Task<bool> AgregarServicioAdicionalContratado(ServicioAdicionalContratado servicioAdicionalContratado);
    }
}
