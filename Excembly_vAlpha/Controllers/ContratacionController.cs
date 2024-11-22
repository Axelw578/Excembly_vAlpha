using AutoMapper;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class ContratacionController : Controller
    {
        private readonly IContratacionService _contratacionService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContratacionController> _logger;

        public ContratacionController(IContratacionService contratacionService, IMapper mapper, ILogger<ContratacionController> logger)
        {
            _contratacionService = contratacionService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarioId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var contrataciones = await _contratacionService.ObtenerContratacionesDelUsuario(usuarioId);

                if (contrataciones == null)
                {
                    _logger.LogWarning($"No se encontraron contrataciones para el usuario con ID {usuarioId}");
                    return View(new List<ContratacionViewModel>());
                }

                var contratacionesViewModel = _mapper.Map<IEnumerable<ContratacionViewModel>>(contrataciones);
                return View(contratacionesViewModel);
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al obtener contrataciones para el usuario.");
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalles(int contratacionId)
        {
            try
            {
                var contratacion = await _contratacionService.ObtenerContratacionPorId(contratacionId);
                if (contratacion == null)
                {
                    _logger.LogWarning($"Contratación con ID {contratacionId} no encontrada");
                    return NotFound();
                }

                var contratacionViewModel = _mapper.Map<ContratacionViewModel>(contratacion);
                return View(contratacionViewModel);
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al obtener detalles de la contratación.");
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            try
            {
                var usuarioId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var nombreUsuario = User.FindFirstValue(ClaimTypes.Name);
                var correoUsuario = User.FindFirstValue(ClaimTypes.Email);

                if (usuarioId <= 0 || string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(correoUsuario))
                {
                    _logger.LogWarning("No se pudieron obtener los datos del usuario autenticado.");
                    return View("Error");
                }

                var viewModel = await CrearContratacionViewModel();
                viewModel.UsuarioId = usuarioId;
                viewModel.NombreUsuario = nombreUsuario;
                viewModel.CorreoUsuario = correoUsuario;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(new
                {
                    Error = "Error al cargar la página de creación",
                    Detalle = ex.Message,
                    StackTrace = ex.StackTrace
                }));
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Crear(ContratacionViewModel contratacionViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contratacion = _mapper.Map<Contratacion>(contratacionViewModel);
                    var resultado = await _contratacionService.AgregarContratacion(contratacion);

                    if (resultado) return RedirectToAction("Index");
                }

                _logger.LogWarning("Problema con el formulario de creación. Reintentando...");
                var viewModel = await CrearContratacionViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al crear contratación.");
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int contratacionId)
        {
            try
            {
                var contratacion = await _contratacionService.ObtenerContratacionPorId(contratacionId);
                if (contratacion == null)
                {
                    _logger.LogWarning($"Contratación con ID {contratacionId} no encontrada");
                    return NotFound();
                }

                var viewModel = await CrearContratacionViewModel();
                var contratacionViewModel = _mapper.Map(contratacion, viewModel);
                return View(contratacionViewModel);
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al cargar página de edición.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ContratacionViewModel contratacionViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contratacion = _mapper.Map<Contratacion>(contratacionViewModel);
                    var resultado = await _contratacionService.EditarContratacion(contratacion);

                    if (resultado) return RedirectToAction("Detalles", new { contratacionId = contratacion.ContratacionId });
                }

                _logger.LogWarning($"Problema al editar contratación con ID {contratacionViewModel.ContratacionId}. Reintentando...");
                return View(await CrearContratacionViewModel());
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al editar contratación.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cancelar(int contratacionId)
        {
            try
            {
                var resultado = await _contratacionService.CancelarContratacion(contratacionId);
                if (resultado) return RedirectToAction("Index");

                _logger.LogWarning($"Problema al cancelar la contratación con ID {contratacionId}");
                return View("Error");
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al cancelar contratación.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SeleccionarPlanOServicio(int contratacionId, int? planId, int? servicioId, List<int>? serviciosAdicionalesIds)
        {
            try
            {
                var resultado = await _contratacionService.SeleccionarPlanOServicio(contratacionId, planId, servicioId, serviciosAdicionalesIds);
                if (resultado) return RedirectToAction("Detalles", new { contratacionId });

                _logger.LogWarning($"Problema al seleccionar plan o servicio para contratación con ID {contratacionId}");
                return View("Error");
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al seleccionar plan o servicio.");
                return View("Error");
            }
        }

        private async Task<ContratacionViewModel> CrearContratacionViewModel()
        {
            var planesDisponibles = await _contratacionService.ObtenerPlanesDisponibles();
            var serviciosDisponibles = await _contratacionService.ObtenerServiciosDisponibles();
            var serviciosAdicionalesDisponibles = await _contratacionService.ObtenerServiciosAdicionalesDisponibles();

            return new ContratacionViewModel
            {
                PlanesDisponibles = _mapper.Map<IEnumerable<PlanViewModel>>(planesDisponibles),
                ServiciosDisponibles = _mapper.Map<IEnumerable<ServicioViewModel>>(serviciosDisponibles),
                ServiciosAdicionalesDisponibles = _mapper.Map<IEnumerable<ServicioAdicionalViewModel>>(serviciosAdicionalesDisponibles)
            };
        }

        private void LogJsonError(Exception ex, string customMessage)
        {
            var errorDetails = new
            {
                Message = customMessage,
                Exception = ex.Message,
                StackTrace = ex.StackTrace,
                InnerException = ex.InnerException?.Message
            };

            _logger.LogError(JsonConvert.SerializeObject(errorDetails, Formatting.Indented));
        }
    }
}
