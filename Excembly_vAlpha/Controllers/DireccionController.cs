using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class DireccionController : Controller
    {
        private readonly DireccionService _direccionService;

        // Inyecta el servicio de direcciones
        public DireccionController(DireccionService direccionService)
        {
            _direccionService = direccionService;
        }

        // Muestra la vista de edición o adición de dirección para un usuario específico
        [HttpGet]
        public async Task<IActionResult> RegistrarDireccion(int usuarioId)
        {
            // Obtiene la dirección del usuario si existe; si no, crea un modelo nuevo con el ID del usuario
            var viewModel = await _direccionService.ObtenerDireccionAsync(usuarioId) ?? new DireccionViewModel { UsuarioId = usuarioId };

            // Retorna la vista con el modelo para llenar o editar la dirección
            return View("~/Views/Direccion/RegistrarDireccion.cshtml", viewModel);
        }

        // Guarda o actualiza la dirección ingresada
        [HttpPost]
        public async Task<IActionResult> GuardarDireccion(DireccionViewModel model)
        {
            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                // Guarda la dirección a través del servicio y verifica el éxito de la operación
                var exito = await _direccionService.GuardarDireccionAsync(model);

                if (exito)
                {
                    TempData["Exito"] = "Dirección guardada con éxito.";
                    return RedirectToAction("Index", "Login"); // Redirige a la página de inicio de sesión después del registro completo
                }

                ModelState.AddModelError("", "No se pudo guardar la dirección. Por favor, inténtalo de nuevo.");
            }

            // Si el modelo no es válido, regresa a la vista con los errores de validación
            return View("~/Views/Direccion/RegistrarDireccion.cshtml", model);
        }
    }
}
