using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ServiciosService _serviciosService;

        public ServiciosController(ServiciosService serviciosService)
        {
            _serviciosService = serviciosService;
        }

        // Acción para mostrar todos los servicios
        public IActionResult Index()
        {
            IEnumerable<ServicioViewModel> servicios = _serviciosService.ObtenerTodosLosServicios();
            return View(servicios); // Enviamos la lista de servicios a la vista
        }

        // Acción para contratar un servicio
        public IActionResult Contratar(int id)
        {
            // Verificar si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Redirigir a la página de inicio de sesión y pasar la URL de retorno
                return RedirectToAction("Index", "Login", new { returnUrl = Url.Action("Contratar", "Servicios", new { id }) });
            }

            // Obtener el servicio por ID
            var servicio = _serviciosService.ObtenerServicioPorId(id);
            if (servicio == null)
            {
                return NotFound();
            }

            // Si el usuario está autenticado, redirigir a la vista de contratación
            return RedirectToAction("Index", "Contratacion", new { servicioId = id });
        }


    }
}