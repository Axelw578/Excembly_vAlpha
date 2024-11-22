using System;
using System.Linq;
using System.Threading.Tasks;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Excembly_vAlpha.Controllers
{
    public class CuentaController : Controller
    {
        private readonly CuentaService _cuentaService;
        private readonly ILogger<CuentaController> _logger;
        private readonly string _rutaImagenGenerica =
"/imagenes/perfil/default-profile.png";

        public CuentaController(CuentaService cuentaService,
ILogger<CuentaController> logger)
        {
            _cuentaService = cuentaService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = ObtenerUsuarioIdDesdeClaims();
            if (usuarioId == null)
            {
                _logger.LogWarning("Usuario no autenticado, redirigiendo al login."); 
                return RedirectToAction("Login", "Auth");
            }

            var cuenta = _cuentaService.ObtenerCuentaPorId(usuarioId.Value);
            if (cuenta == null)
            {
                _logger.LogWarning("No se encontró la cuenta para el usuario con ID { UsuarioId}.", usuarioId); 
                return NotFound("La cuenta no fue encontrada.");
            }

            var viewModel = MapearCuentaAVista(cuenta);
            _logger.LogInformation("Cuenta cargada correctamente para el usuario con ID { UsuarioId}.", usuarioId); 
            return View(viewModel);
        }

        private CuentaViewModel MapearCuentaAVista(Usuario cuenta)
        {
            return new CuentaViewModel
            {
                UsuarioId = cuenta.UsuarioId,
                Nombre = cuenta.Nombre,
                Apellidos = cuenta.Apellidos,
                CorreoElectronico = cuenta.CorreoElectronico,
                Telefono = cuenta.Telefono,
                FotoPerfilUrl = string.IsNullOrEmpty(cuenta.FotoPerfilUrl) ?
_rutaImagenGenerica : cuenta.FotoPerfilUrl
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(CuentaViewModel cuentaViewModel)
        {
            // Validar el modelo 
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El modelo no es válido para la actualización del perfil."); 

                // Mostrar errores de validación en TempData 
                TempData["Errores"] =
Newtonsoft.Json.JsonConvert.SerializeObject(ModelState.Values
                    .Where(x => x.Errors.Count > 0)
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));

                return View("Index", cuentaViewModel);
            }

            try
            {
                // Validar el teléfono: debe tener exactamente 10 dígitos 
                if
(!System.Text.RegularExpressions.Regex.IsMatch(cuentaViewModel.Telefono,
@"^\d{10}$"))
                {
                    ModelState.AddModelError("Telefono", "El teléfono debe tener 10 dígitos."); 
                    _logger.LogWarning("Teléfono inválido para el usuario con ID { UsuarioId}.", cuentaViewModel.UsuarioId); 

                    // Mostrar el error y regresar a la vista 
                    TempData["Errores"] =
Newtonsoft.Json.JsonConvert.SerializeObject(ModelState.Values
                        .Where(x => x.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));
                    return View("Index", cuentaViewModel);
                }

                // Validar que la URL de la imagen no esté vacía 
                if (string.IsNullOrEmpty(cuentaViewModel.FotoPerfilUrl))
                {
                    cuentaViewModel.FotoPerfilUrl = _rutaImagenGenerica;
                    _logger.LogInformation("Se ha asignado la foto de perfil genérica para el usuario con ID { UsuarioId}.", cuentaViewModel.UsuarioId); 
                }

                // Mapear el ViewModel a la entidad Usuario 
                var cuentaModelo = new Usuario
                {
                    UsuarioId = cuentaViewModel.UsuarioId,
                    Nombre = cuentaViewModel.Nombre,
                    Apellidos = cuentaViewModel.Apellidos,
                    Telefono = cuentaViewModel.Telefono,
                    FotoPerfilUrl = cuentaViewModel.FotoPerfilUrl
                };

                // Actualizar la cuenta a través del servicio 
                _cuentaService.ActualizarCuenta(cuentaModelo);
                // Actualizar los Claims del usuario autenticado 
                var identity = (ClaimsIdentity)User.Identity;
                var fotoClaim = identity.FindFirst("URLFotoPerfil");
                if (fotoClaim != null)
                {
                    identity.RemoveClaim(fotoClaim);
                }
                identity.AddClaim(new Claim("URLFotoPerfil",
cuentaViewModel.FotoPerfilUrl));

                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);

                // Mensaje de éxito y redirigir 
                TempData["Exito"] = "Perfil actualizado correctamente.";
                _logger.LogInformation("Perfil actualizado exitosamente para el usuario con ID { UsuarioId}.", cuentaViewModel.UsuarioId); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y mostrar el error 
                _logger.LogError(ex, "Error al intentar actualizar el perfil del usuario con ID { UsuarioId}.", cuentaViewModel.UsuarioId); 

                // Guardar el error en TempData 
                TempData["Errores"] =
Newtonsoft.Json.JsonConvert.SerializeObject(new
{
    mensaje = ex.Message,
    stackTrace = ex.StackTrace
});
                return View("Index", cuentaViewModel);
            }
        }

        private int? ObtenerUsuarioIdDesdeClaims()
        {
            var usuarioIdStr =
User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(usuarioIdStr, out var usuarioId) ? usuarioId :
(int?)null;
        }
    }
}