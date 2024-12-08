using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Excembly_vAlpha.Controllers
{
    public class ContratacionTecnicoController : Controller
    {
        private readonly IContratacionAdminServices _contratacionService;

        public ContratacionTecnicoController(IContratacionAdminServices contratacionService)
        {
            _contratacionService = contratacionService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Completar(int contratacionId)
        {
            try
            {
                // Llamar al servicio para marcar la contratación como completada
                var resultado = await _contratacionService.MarcarComoCompletadoAsync(contratacionId);

                if (!resultado)
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return View("Error", new { message = "Hubo un problema al completar la contratación." });
            }
        }

        // Mostrar todas las contrataciones
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtener contrataciones activas y activas de todos los clientes
                var contrataciones = await _contratacionService.ObtenerContratacionesActivasAsync();
                return View(contrataciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return View("Error", new { message = "Hubo un problema al cargar las contrataciones activas." });
            }
        }

        // Redirigir a la vista de agregar comentario
        [HttpGet]
        public IActionResult RedirigirAgregarComentario(int contratacionId)
        {
            return RedirectToAction("Agregar", "Comentario", new { contratacionId });
        }

        //Filtrar Tecnico
        [HttpGet]
        public async Task<IActionResult> Filtrar(DateTime? fechaInicio, DateTime? fechaFin, int? usuarioId)
        {
            try
            {
                // Obtener contrataciones activas y activas con los filtros aplicados
                var contratacionesFiltradas = await _contratacionService.FiltrarContratacionesActivasAsync(fechaInicio, fechaFin, usuarioId);

                // Devuelve la vista con los resultados filtrados
                return View("Index", contratacionesFiltradas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return View("Error", new { message = "Hubo un problema al filtrar las contrataciones activas." });
            }
        }
    }
}
