using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class ContratacionAdminController : Controller
    {
        private readonly IContratacionAdminServices _contratacionService;

        public ContratacionAdminController(IContratacionAdminServices contratacionService)
        {
            _contratacionService = contratacionService;
        }

        // 1. Vista principal: listar todas las contrataciones
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

        // 2. Filtro de contrataciones
        [HttpGet]
        public async Task<IActionResult> Filtrar(DateTime? fechaInicio, DateTime? fechaFin, int? usuarioId)
        {
            try
            {
                var contrataciones = await _contratacionService.FiltrarContratacionesAsync(fechaInicio, fechaFin, usuarioId);
                return PartialView("_ListaContrataciones", contrataciones); // Retorna una vista parcial con los resultados
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return BadRequest("Hubo un problema al filtrar las contrataciones.");
            }
        }

        // 3. Detalle de una contratación
        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            try
            {
                var contratacion = await _contratacionService.ObtenerDetalleContratacionAsync(id);
                var pagos = await _contratacionService.ObtenerPagosPorContratacionAsync(id);

                var detalleViewModel = new
                {
                    Contratacion = contratacion,
                    Pagos = pagos
                };

                return View(detalleViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return View("Error", new { message = "Hubo un problema al cargar el detalle de la contratación." });
            }
        }

        // 4. Vista para asignar técnico
        [HttpGet]
        public async Task<IActionResult> AsignarTecnico(int contratacionId)
        {
            try
            {
                // Obtener la lista de técnicos disponibles
                var tecnicos = await _contratacionService.ObtenerTecnicosAsync();

                var viewModel = new AsignarTecnicoViewModel
                {
                    ContratacionId = contratacionId,
                    Tecnicos = tecnicos.Select(t => new TecnicoViewModel
                    {
                        Nombre = t.Nombre,
                        Apellidos = t.Apellidos,
                        Disponibilidad = t.Disponibilidad
                    }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return View("Error", new { message = "Hubo un problema al cargar los técnicos disponibles." });
            }
        }

        // 5. Asignar técnico (POST)
        [HttpPost]
        public async Task<IActionResult> AsignarTecnico(AsignarTecnicoViewModel model)
        {
            try
            {
                // Validar que el técnico haya sido seleccionado
                if (model.TecnicoId == 0)
                {
                    TempData["ErrorMessage"] = "Por favor seleccione un técnico.";
                    return RedirectToAction("AsignarTecnico", new { contratacionId = model.ContratacionId });
                }

                // Llamar al servicio para asignar el técnico
                var resultado = await _contratacionService.AsignarTecnicoAsync(model.ContratacionId, model.TecnicoId);

                // Verificar si la asignación fue exitosa
                if (resultado)
                {
                    TempData["SuccessMessage"] = "Técnico asignado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo asignar el técnico.";
                }

                // Redirigir a la vista de detalles
                return RedirectToAction("Detalle", new { id = model.ContratacionId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                TempData["ErrorMessage"] = "Hubo un problema al asignar el técnico.";
                return RedirectToAction("AsignarTecnico", new { contratacionId = model.ContratacionId });
            }
        }
    }
}
 