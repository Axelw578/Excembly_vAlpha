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
            if (!User.Identity.IsAuthenticated)
            {
                // Redirige a la página de registro con la URL de retorno
                return RedirectToAction("Registrar", "Registro", new { returnUrl = Url.Action("Contratar", "Servicios", new { id = id }) });
            }

            var servicio = _serviciosService.ObtenerServicioPorId(id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View("Contratacion", servicio);
        }
    }
}
