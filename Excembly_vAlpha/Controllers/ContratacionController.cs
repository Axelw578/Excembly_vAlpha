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
                    StackTrace = ex.StackTrace,
                    UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    NombreUsuario = User.FindFirstValue(ClaimTypes.Name)
                }));
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ContratacionViewModel contratacionViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("El formulario de creación no es válido.");
                    return View(await CrearContratacionViewModel());
                }

                // Mapear el ViewModel a la entidad Contratacion
                var contratacion = _mapper.Map<Contratacion>(contratacionViewModel);

                // Validar y asignar el plan o servicio principal
                if (contratacionViewModel.PlanId.HasValue)
                {
                    var plan = await _contratacionService.ObtenerPlanPorId(contratacionViewModel.PlanId.Value);
                    if (plan == null)
                    {
                        _logger.LogWarning($"Plan con ID {contratacionViewModel.PlanId} no encontrado.");
                        return View("Error");
                    }
                    contratacion.PlanId = plan.PlanId;
                    contratacion.Estado = "Activa";
                }
                else if (contratacionViewModel.ServicioId.HasValue)
                {
                    var servicio = await _contratacionService.ObtenerServicioPorId(contratacionViewModel.ServicioId.Value);
                    if (servicio == null)
                    {
                        _logger.LogWarning($"Servicio con ID {contratacionViewModel.ServicioId} no encontrado.");
                        return View("Error");
                    }
                    contratacion.ServicioId = servicio.ServicioId;
                    contratacion.Estado = "Activa";
                }
                else
                {
                    _logger.LogWarning("No se seleccionó ni un plan ni un servicio principal.");
                    return View("Error");
                }

                // Guardar la contratación en la base de datos
                var resultadoContratacion = await _contratacionService.AgregarContratacion(contratacion);
                if (!resultadoContratacion)
                {
                    _logger.LogWarning("No se pudo guardar la contratación.");
                    return View("Error");
                }

                // Obtener el ID de la contratación recién creada
                var contratacionId = contratacion.ContratacionId;

                // Asociar los servicios adicionales seleccionados
                if (contratacionViewModel.ServiciosAdicionalesSeleccionados != null &&
                    contratacionViewModel.ServiciosAdicionalesSeleccionados.Any())
                {
                    foreach (var servicioAdicionalId in contratacionViewModel.ServiciosAdicionalesSeleccionados)
                    {
                        var servicioAdicional = await _contratacionService.ObtenerServicioAdicionalPorId(servicioAdicionalId);
                        if (servicioAdicional == null)
                        {
                            _logger.LogWarning($"Servicio adicional con ID {servicioAdicionalId} no encontrado. Se omitirá.");
                            continue;
                        }

                        var servicioAdicionalContratado = new ServicioAdicionalContratado
                        {
                            ContratacionId = contratacionId,
                            ServicioAdicionalId = servicioAdicional.ServicioId,
                            DescuentoAplicado = servicioAdicional.Descuento
                        };

                        var resultadoServicio = await _contratacionService.AgregarServicioAdicionalContratado(servicioAdicionalContratado);
                        if (!resultadoServicio)
                        {
                            _logger.LogWarning($"No se pudo guardar el servicio adicional con ID {servicioAdicionalId} para la contratación {contratacionId}.");
                        }
                    }
                }

                _logger.LogInformation($"Contratación creada exitosamente con ID: {contratacionId}");
                return RedirectToAction("Detalles", new { contratacionId });
            }
            catch (Exception ex)
            {
                LogJsonError(ex, "Error al crear la contratación.");
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
                if (contratacionId <= 0)
                {
                    _logger.LogWarning("ID de contratación inválido al seleccionar plan o servicio.");
                    return BadRequest("ID de contratación inválido.");
                }

                // Validar si hay al menos un plan o servicio seleccionado
                if (!planId.HasValue && !servicioId.HasValue && (serviciosAdicionalesIds == null || serviciosAdicionalesIds.Count == 0))
                {
                    _logger.LogWarning("No se seleccionó un plan, servicio o servicios adicionales para la contratación.");
                    return BadRequest("Debe seleccionar al menos un plan o servicio.");
                }

                // Validar servicios adicionales seleccionados
                var serviciosAdicionalesValidos = new List<int>();
                if (serviciosAdicionalesIds != null)
                {
                    foreach (var servicioAdicionalId in serviciosAdicionalesIds)
                    {
                        var servicioAdicional = await _contratacionService.ObtenerServicioAdicionalPorId(servicioAdicionalId);
                        if (servicioAdicional != null)
                        {
                            serviciosAdicionalesValidos.Add(servicioAdicionalId);
                        }
                        else
                        {
                            _logger.LogWarning($"Servicio adicional con ID {servicioAdicionalId} no encontrado. Ignorando.");
                        }
                    }
                }

                // Llamar al servicio para actualizar la contratación
                var resultado = await _contratacionService.SeleccionarPlanOServicio(contratacionId, planId, servicioId, serviciosAdicionalesValidos);

                if (resultado)
                {
                    _logger.LogInformation($"Plan/servicio actualizado para la contratación con ID {contratacionId}.");
                    return RedirectToAction("Detalles", new { contratacionId });
                }

                _logger.LogWarning($"Problema al seleccionar plan o servicio para contratación con ID {contratacionId}.");
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
