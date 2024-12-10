using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Excembly_vAlpha.Services;

namespace Excembly_vAlpha.Controllers
{
    public class PruebaServiciosController : Controller
    {
        private readonly IContratacionService _contratacionService;

        // Constructor con inyección de dependencias
        public PruebaServiciosController(IContratacionService contratacionService)
        {
            _contratacionService = contratacionService;
        }

        // Acción Index para mostrar los servicios adicionales contratados
        public async Task<IActionResult> Index(int contratacionId)
        {
            if (contratacionId <= 0)
            {
                return BadRequest("ID de contratación no válido.");
            }

            // Obtenemos los servicios adicionales contratados
            var serviciosAdicionales = await _contratacionService.ObtenerServiciosAdicionalesContratadosPorContratacionId(contratacionId);

            if (serviciosAdicionales == null)
            {
                return NotFound("No se encontraron servicios adicionales para esta contratación.");
            }

            // Pasamos los datos a la vista
            return View(serviciosAdicionales);
        }
    }
}
