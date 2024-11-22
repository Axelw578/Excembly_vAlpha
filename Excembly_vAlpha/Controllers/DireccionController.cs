using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    [Authorize]
    public class DireccionController : Controller
    {
        private readonly DireccionService _direccionService;
        private readonly ILogger<DireccionController> _logger;

        public DireccionController(DireccionService direccionService, ILogger<DireccionController> logger)
        {
            _direccionService = direccionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    _logger.LogWarning("No se encontró el claim de ID del usuario. Redirigiendo al login.");
                    return RedirectToAction("Login", "Account");
                }

                int usuarioId = int.Parse(userIdClaim.Value);
                var direccion = await _direccionService.ObtenerDireccionAsync(usuarioId);

                if (direccion == null)
                {
                    direccion = new DireccionViewModel
                    {
                        UsuarioId = usuarioId,
                        TieneDireccion = false
                    };
                }

                return View("Index", direccion);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la vista Index de dirección.");
                TempData["Error"] = "Ocurrió un error al intentar cargar la información de la dirección.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarDireccion()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    _logger.LogWarning("No se encontró el claim de ID del usuario. Redirigiendo al login.");
                    return RedirectToAction("Login", "Account");
                }

                int usuarioId = int.Parse(userIdClaim.Value);

                var direccion = new DireccionViewModel
                {
                    UsuarioId = usuarioId
                };

                return View("RegistrarDireccion", direccion);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la vista de registro de dirección.");
                TempData["Error"] = "Ocurrió un error al intentar registrar la dirección.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarDireccion(DireccionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("RegistrarDireccion", model);
                }

                var exito = await _direccionService.GuardarDireccionAsync(model);

                if (exito)
                {
                    TempData["Exito"] = "Dirección registrada con éxito.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "No se pudo registrar la dirección. Por favor, inténtalo de nuevo.");
                return View("RegistrarDireccion", model);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la dirección.");
                TempData["Error"] = "Ocurrió un error al guardar la dirección.";
                return View("RegistrarDireccion", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarDireccion(int direccionId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                int usuarioId = int.Parse(userIdClaim.Value);
                var direccion = await _direccionService.ObtenerDireccionAsync(usuarioId);

                if (direccion == null || direccion.DireccionId != direccionId)
                {
                    TempData["Error"] = "No se encontró la dirección especificada.";
                    return RedirectToAction("Index");
                }

                return View("EditarDireccion", direccion);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la vista de edición de dirección.");
                TempData["Error"] = "Ocurrió un error al intentar editar la dirección.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarDireccion(DireccionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("EditarDireccion", model);
                }

                var exito = await _direccionService.EditarDireccionAsync(model);

                if (exito)
                {
                    TempData["Exito"] = "Dirección actualizada con éxito.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "No se pudo actualizar la dirección. Por favor, inténtalo de nuevo.");
                return View("EditarDireccion", model);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la dirección.");
                TempData["Error"] = "Ocurrió un error al actualizar la dirección.";
                return View("EditarDireccion", model);
            }
        }
    }
}
