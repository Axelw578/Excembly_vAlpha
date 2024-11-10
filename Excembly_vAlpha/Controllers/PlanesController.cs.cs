using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
 
namespace Excembly_vAlpha.Controllers
{
    public class PlanesController : Controller
    {
        private readonly PlanesService _planesService;
 
        public PlanesController(PlanesService planesService)
        {
            _planesService = planesService;
        }
 
        // Acción para mostrar todos los planes
        public ActionResult Index()
        {
            // Obtener los planes desde el servicio
            var planes = _planesService.ObtenerTodosLosPlanes();
 
            // Convertir los datos de los planes en una lista de PlanViewModel
            var planesViewModel = planes.Select(plan => new PlanViewModel
            {
                PlanId = plan.PlanId,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                Imagen = plan.Imagen,
 
                // Convertir los servicios incluidos en el plan
                ServiciosIncluidos = plan.PlanServicios?.Select(ps => ps.Servicio.Nombre).ToList() ?? new List<string>(),
 
                // Convertir los servicios adicionales con descuento en un nuevo ViewModel
                ServiciosAdicionales = plan.ServiciosAdicionales?.Select(sa => new ServicioAdicionalViewModel
                {
                    NombreServicio = sa.Servicio.Nombre,
                    PrecioOriginal = sa.Servicio.Precio,
                    PrecioConDescuento = sa.Servicio.Precio * (1 - sa.Descuento), // Calculando el precio con descuento
                    Descuento = sa.Descuento
                }).ToList() ?? new List<ServicioAdicionalViewModel>()
            }).ToList();
 
            // Pasar el modelo a la vista
            return View(planesViewModel);
        }
 
        // Acción para contratar un plan
        public IActionResult Contratar(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirige a la página de registro con la URL de retorno a esta acción de Contratar en Planes
                return RedirectToAction("Registrar", "Registro", new { returnUrl = Url.Action("Contratar", "Planes", new { id = id }) });
            }
 
            // Obtener el plan específico por su ID
            var servicio = _planesService.ObtenerPlanPorId(id);
            if (servicio == null)
            {
                return NotFound();
            }
 
            // Redirige a la vista de contratación del plan específico
            return View("Contratacion", servicio);
        }
    }
}
