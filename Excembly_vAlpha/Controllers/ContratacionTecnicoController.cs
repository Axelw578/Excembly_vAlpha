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
                var contrataciones = await _contratacionService.ObtenerTodasContratacionesAsync();
                return View(contrataciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return View("Error", new { message = "Hubo un problema al cargar las contrataciones." });
            }
        }

        // Redirigir a la vista de agregar comentario
        [HttpGet]
        public IActionResult RedirigirAgregarComentario(int contratacionId)
        {
            return RedirectToAction("Agregar", "Comentario", new { contratacionId });
        }
    }
}
