using AutoMapper;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class PlanesController : Controller
    {
        private readonly PlanesService _planesService;
        private readonly IMapper _mapper;

        public PlanesController(PlanesService planesService, IMapper mapper)
        {
            _planesService = planesService;
            _mapper = mapper;
        }

        // Acción para mostrar todos los planes
        public async Task<IActionResult> Index()
        {
            // Obtener los planes desde el servicio
            var planes = await _planesService.ObtenerTodosLosPlanesAsync();

            // Convertir los datos de los planes en una lista de PlanViewModel usando AutoMapper
            var planesViewModel = _mapper.Map<List<PlanViewModel>>(planes);

            // Pasar el modelo a la vista
            return View(planesViewModel);
        }

        // Acción para contratar un plan
        // Controlador de Planes
        public async Task<IActionResult> Contratar(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirige a registro si no está autenticado
                return RedirectToAction("Registrar", "Registro", new { returnUrl = Url.Action("Contratar", "Planes", new { id }) });
            }

            // Obtener el plan por ID
            var plan =  _planesService.ObtenerPlanPorId(id);
            if (plan == null)
            {
                return NotFound();
            }

            // Redirige a la acción Crear del controlador Contratación pasando el planId
            return RedirectToAction("Index", "Contratacion", new { planId = id });
        }


        // Acción para crear una contratación (esto se llamará desde la vista de Contratación)
        [HttpPost]
        public async Task<IActionResult> CrearContratacion(int planId, List<int> serviciosAdicionalesSeleccionados)
        {
            // Verificar que el usuario esté autenticado
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // Guardar la contratación del plan
            var contratacionId = await _planesService.GuardarContratacionAsync(planId, User.Identity.Name);

            // Guardar los servicios adicionales seleccionados, si hay alguno
            if (serviciosAdicionalesSeleccionados != null && serviciosAdicionalesSeleccionados.Any())
            {
                await _planesService.GuardarServiciosAdicionalesAsync(contratacionId, serviciosAdicionalesSeleccionados);
            }

            // Redirigir a la vista de confirmación de la contratación
            return RedirectToAction("Confirmacion", "Contratacion", new { id = contratacionId });
        }
    }
}
