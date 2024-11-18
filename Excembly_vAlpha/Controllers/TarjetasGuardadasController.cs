using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using System;
using System.Collections.Generic;

namespace Excembly_vAlpha.Controllers
{
    public class TarjetasGuardadasController : Controller
    {
        private readonly TarjetaService _tarjetaService;
        private readonly PoliticaService _politicaService;
        private readonly ILogger<TarjetasGuardadasController> _logger;

        public TarjetasGuardadasController(TarjetaService tarjetaService, PoliticaService politicaService, ILogger<TarjetasGuardadasController> logger)
        {
            _tarjetaService = tarjetaService;
            _politicaService = politicaService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Login");
                }

                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var tarjetas = await _tarjetaService.ObtenerTarjetasGuardadasAsync(usuarioId);
                var politicas = await _politicaService.ObtenerPoliticasAsync();

                var viewModel = new MetodoPagoViewModel
                {
                    TarjetasGuardadas = tarjetas.Select(t => new TarjetaViewModel
                    {
                        NombreTitular = t.NombreTitular,
                        NumeroTarjeta = t.NumeroTarjeta,
                        MesExpiracion = t.FechaExpiracion.Month,
                        AñoExpiracion = t.FechaExpiracion.Year,
                        CVV = t.CVV,
                        Banco = t.Banco,
                        Marca = t.Marca
                    }).ToList(),
                    Politica = new PoliticaViewModel
                    {
                        PoliticasCancelacionReembolso = politicas?.ConvertAll(p => p.Contenido) ?? new List<string> { "No hay políticas disponibles." }
                    }
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la vista de tarjetas guardadas.");
                return View("Error");
            }
        }


        public IActionResult AgregarTarjeta()
        {
            return View("AgregarTarjeta");
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTarjeta(TarjetaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                    {
                        return RedirectToAction("Index", "Login");
                    }

                    var tarjeta = new TarjetaGuardada
                    {
                        UsuarioId = usuarioId,
                        NombreTitular = model.NombreTitular,
                        NumeroTarjeta = model.NumeroTarjeta,
                        FechaExpiracion = new DateTime(model.AñoExpiracion, model.MesExpiracion, 1),
                        CVV = model.CVV,
                        Banco = model.Banco,
                        Marca = model.Marca,
                        TipoTarjeta = model.TipoTarjeta  // upps
                    };

                    // Intenta agregar la tarjeta y maneja el resultado
                    var resultado = await _tarjetaService.AgregarTarjetaAsync(tarjeta);

                    if (!resultado.IsSuccess)
                    {
                        // Si hay un error, añade el mensaje de error específico a ModelState
                        ModelState.AddModelError("", resultado.ErrorMessage);
                        return View("AgregarTarjeta", model);
                    }

                    return RedirectToAction("Index");
                }

                return View("AgregarTarjeta", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar tarjeta.");
                ModelState.AddModelError("", $"Error específico: {ex.InnerException?.Message ?? ex.Message}");
                return View("AgregarTarjeta", model);
            }
        }



        public async Task<IActionResult> Editar(int tarjetaId)
        {
            try
            {
                var tarjeta = await _tarjetaService.SeleccionarTarjetaAsync(tarjetaId);
                if (tarjeta == null)
                {
                    return RedirectToAction("Index");
                }

                // Mapea el modelo a la vista
                var tarjetaViewModel = new TarjetaViewModel
                {
                    NombreTitular = tarjeta.NombreTitular,
                    NumeroTarjeta = tarjeta.NumeroTarjeta,
                    MesExpiracion = tarjeta.FechaExpiracion.Month,
                    AñoExpiracion = tarjeta.FechaExpiracion.Year,
                    CVV = tarjeta.CVV,
                    Banco = tarjeta.Banco,
                    Marca = tarjeta.Marca,
                    TipoTarjeta = tarjeta.TipoTarjeta
                };

                return View("EditarTarjeta", tarjetaViewModel); // Reutiliza la vista de editar
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la tarjeta para editar. Tarjeta ID: {TarjetaId}", tarjetaId);
                return View("Error");
            }
        }





        // Eliminar tarjeta
        [HttpPost]
        public async Task<IActionResult> Eliminar(int tarjetaId)
        {
            try
            {
                var usuarioId = ObtenerUsuarioId(); // Verificar que esto no sea null
                if (usuarioId == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var tarjeta = await _tarjetaService.ObtenerTarjetaPorUsuarioIdAsync(usuarioId.Value);
                if (tarjeta == null || tarjeta.TarjetaId != tarjetaId)
                {
                    return Json(new { success = false, message = "No se encontró la tarjeta o no tienes permisos para eliminarla." });
                }

                var eliminado = await _tarjetaService.EliminarTarjetaAsync(tarjetaId);
                if (eliminado)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "No se pudo eliminar la tarjeta." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tarjeta.");
                return Json(new { success = false, message = "Ocurrió un error inesperado." });
            }
        }



        public async Task<IActionResult> Seleccionar(int tarjetaId)
        {
            try
            {
                var tarjeta = await _tarjetaService.SeleccionarTarjetaAsync(tarjetaId);
                if (tarjeta == null)
                {
                    return RedirectToAction("Index");
                }

                return View("ConfirmarMetodoDePago", tarjeta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al seleccionar tarjeta.");
                return View("Error");
            }
        }




        public async Task<IActionResult> ConfirmarMetodoDePago(int tarjetaId)
        {
            try
            {
                var tarjeta = await _tarjetaService.SeleccionarTarjetaAsync(tarjetaId);
                var politicas = await _politicaService.ObtenerPoliticasAsync();

                ViewBag.Politicas = politicas?.ConvertAll(p => p.Contenido) ?? new List<string> { "No hay políticas disponibles." };
                ViewBag.TarjetaCensurada = tarjeta != null ? $"**** **** **** {tarjeta.NumeroTarjeta[^4..]}" : "Tarjeta no encontrada";

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al confirmar método de pago.");
                return View("Error");
            }
        }

        private int? ObtenerUsuarioId()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return usuarioId;
            }
            return null;
        }

    }
}
