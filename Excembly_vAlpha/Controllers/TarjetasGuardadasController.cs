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
                        TarjetaId = t.TarjetaId, 
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
                var usuarioId = ObtenerUsuarioId();
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

        // Editar tarjeta
        [HttpGet]
        public async Task<IActionResult> Editar(int tarjetaId)
        {
            try
            {
                // Recuperar el usuario actual
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return RedirectToAction("Index", "Login");
                }

                // Recuperar la tarjeta por ID
                var tarjeta = await _tarjetaService.ObtenerTarjetaPorIdAsync(tarjetaId);
                if (tarjeta == null || tarjeta.UsuarioId != usuarioId) // Verifica que la tarjeta pertenece al usuario
                {
                    return RedirectToAction("Index"); // Si no es la tarjeta del usuario, redirigir al índice
                }

                // Mapear los datos de la tarjeta al nuevo ViewModel de edición
                var editarTarjetaViewModel = new EditarTarjetaViewModel
                {
                    TarjetaId = tarjeta.TarjetaId,
                    NombreTitular = tarjeta.NombreTitular,
                    NumeroTarjeta = tarjeta.NumeroTarjeta,
                    MesExpiracion = tarjeta.FechaExpiracion.Month,
                    AñoExpiracion = tarjeta.FechaExpiracion.Year,
                    CVV = tarjeta.CVV,
                    Banco = tarjeta.Banco,
                    Marca = tarjeta.Marca,
                    TipoTarjeta = tarjeta.TipoTarjeta
                };

                return View(editarTarjetaViewModel); // Devuelve la vista con el modelo adecuado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar tarjeta para editar.");
                return View("Error"); // Si ocurre un error, muestra la vista de error
            }
        }


        [HttpPost]
        public async Task<IActionResult> Editar(int tarjetaId, EditarTarjetaViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model); 
                }

                // Verificar que la fecha de expiración es válida
                if (model.MesExpiracion < 1 || model.MesExpiracion > 12)
                {
                    ModelState.AddModelError("MesExpiracion", "El mes de expiración debe estar entre 1 y 12.");
                    return View(model);
                }

                if (model.AñoExpiracion < DateTime.Now.Year || (model.AñoExpiracion == DateTime.Now.Year && model.MesExpiracion < DateTime.Now.Month))
                {
                    ModelState.AddModelError("AñoExpiracion", "La tarjeta no puede estar expirado.");
                    return View(model);
                }

                // Mapear los datos del ViewModel a una entidad de TarjetaGuardada
                var tarjetaActualizada = new TarjetaGuardada
                {
                    TarjetaId = tarjetaId,
                    NombreTitular = model.NombreTitular,
                    NumeroTarjeta = model.NumeroTarjeta,
                    FechaExpiracion = new DateTime(model.AñoExpiracion, model.MesExpiracion, 1), // Crear la fecha con mes y año
                    CVV = model.CVV,
                    Banco = model.Banco,
                    Marca = model.Marca,
                    TipoTarjeta = model.TipoTarjeta
                };

                // Llamar al servicio para actualizar la tarjeta
                var resultado = await _tarjetaService.EditarTarjetaAsync(tarjetaId, tarjetaActualizada);

                if (resultado.IsSuccess)
                {
                    TempData["SuccessMessage"] = "La tarjeta ha sido actualizada correctamente."; 
                    return RedirectToAction("Index"); // Redirigir al índice después de guardar
                }

                // Si hay un error, añade un mensaje de error
                ModelState.AddModelError("", resultado.ErrorMessage ?? "No se pudo editar la tarjeta.");
                return View(model); // Si algo salió mal, vuelve a mostrar el formulario
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar tarjeta.");
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                return View(model); // Si hay un error, vuelve a mostrar el formulario
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
