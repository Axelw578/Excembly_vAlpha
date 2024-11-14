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





        [HttpPost]
        public async Task<IActionResult> Eliminar(int tarjetaId)
        {
            try
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return RedirectToAction("Index", "Login");
                }

                var tarjeta = await _tarjetaService.ObtenerTarjetaPorUsuarioIdAsync(usuarioId);
                if (tarjeta != null && tarjeta.TarjetaId == tarjetaId && await _tarjetaService.EliminarTarjetaAsync(tarjetaId))
                {
                    return RedirectToAction("Agregar");
                }

                _logger.LogWarning("Tarjeta no encontrada o no autorizada para eliminación.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tarjeta.");
                return View("Error");
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
    }
}
