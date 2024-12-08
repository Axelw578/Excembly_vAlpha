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
using Microsoft.AspNetCore.Identity;

namespace Excembly_vAlpha.Controllers
{
    public class CuentaController : Controller
    {
        private readonly CuentaService _cuentaService;
        private readonly ILogger<CuentaController> _logger;
        private readonly string _rutaImagenGenerica = "/imagenes/perfil/default-profile.png";

        public CuentaController(CuentaService cuentaService, ILogger<CuentaController> logger)
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
                FotoPerfilUrl = string.IsNullOrEmpty(cuenta.FotoPerfilUrl) ? _rutaImagenGenerica : cuenta.FotoPerfilUrl
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(CuentaViewModel cuentaViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El modelo no es válido para la actualización del perfil.");
                TempData["Errores"] = Newtonsoft.Json.JsonConvert.SerializeObject(
                    ModelState.Values
                        .Where(x => x.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                );
                return View("Index", cuentaViewModel);
            }

            try
            {
                // Validar el teléfono
                if (!System.Text.RegularExpressions.Regex.IsMatch(cuentaViewModel.Telefono, @"^\d{10}$"))
                {
                    ModelState.AddModelError("Telefono", "El teléfono debe tener 10 dígitos.");
                    TempData["Errores"] = Newtonsoft.Json.JsonConvert.SerializeObject(
                        ModelState.Values
                            .Where(x => x.Errors.Count > 0)
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                    );
                    return View("Index", cuentaViewModel);
                }

                if (string.IsNullOrEmpty(cuentaViewModel.FotoPerfilUrl))
                {
                    cuentaViewModel.FotoPerfilUrl = _rutaImagenGenerica;
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

                _cuentaService.ActualizarCuenta(cuentaModelo);

                // Actualizar los Claims
                var identity = (ClaimsIdentity)User.Identity;

                // Actualizar claim de nombre
                var nameClaim = identity.FindFirst(ClaimTypes.Name);
                if (nameClaim != null)
                {
                    identity.RemoveClaim(nameClaim);
                }
                identity.AddClaim(new Claim(ClaimTypes.Name, cuentaViewModel.Nombre));

                // Actualizar claim de foto de perfil
                var fotoClaim = identity.FindFirst("URLFotoPerfil");
                if (fotoClaim != null)
                {
                    identity.RemoveClaim(fotoClaim);
                }
                identity.AddClaim(new Claim("URLFotoPerfil", cuentaViewModel.FotoPerfilUrl));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync(principal);

                TempData["Exito"] = "Perfil actualizado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar actualizar el perfil del usuario con ID { UsuarioId}.", cuentaViewModel.UsuarioId);
                TempData["Errores"] = Newtonsoft.Json.JsonConvert.SerializeObject(new { mensaje = ex.Message, stackTrace = ex.StackTrace });
                return View("Index", cuentaViewModel);
            }
        }

        private int? ObtenerUsuarioIdDesdeClaims()
        {
            var usuarioIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(usuarioIdStr, out var usuarioId) ? usuarioId : (int?)null;
        }
    }
}
