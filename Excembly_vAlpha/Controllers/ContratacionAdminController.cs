﻿using Excembly_vAlpha.Services;
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
                // Llama al servicio para filtrar las contrataciones
                var contrataciones = await _contratacionService.FiltrarContratacionesAsync(fechaInicio, fechaFin, usuarioId);

                // Devuelve la vista completa con los datos filtrados
                return View("Index", contrataciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                TempData["ErrorMessage"] = "Hubo un problema al filtrar las contrataciones.";
                return RedirectToAction("Index");
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
                        TecnicoId = t.TecnicoId,
                        Nombre = t.Nombre,
                        Apellidos = t.Apellidos
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
                Console.WriteLine($"ContratacionId: {model.ContratacionId}");
                Console.WriteLine($"TecnicoId: {model.TecnicoId}");

                // Validar que el modelo sea válido solo para los campos relevantes
                if (!ModelState.IsValid || model.TecnicoId == 0)
                {
                    TempData["ErrorMessage"] = "Por favor seleccione un técnico.";
                    return RedirectToAction("AsignarTecnico", new { contratacionId = model.ContratacionId });
                }

                // Llamar al servicio para asignar el técnico
                var resultado = await _contratacionService.AsignarTecnicoAsync(model.ContratacionId, model.TecnicoId);

                if (resultado)
                {
                    TempData["SuccessMessage"] = "Técnico asignado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo asignar el técnico.";
                }

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
