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
                // Llama al servicio para obtener los detalles de la contratación
                var contratacionViewModel = await _contratacionService.ObtenerDetallesDeContratacion(contratacionId);

                // Verifica si la contratación fue encontrada
                if (contratacionViewModel == null)
                {
                    _logger.LogWarning($"Contratación con ID {contratacionId} no encontrada.");
                    return NotFound();
                }

                // Retorna la vista con el ViewModel
                return View(contratacionViewModel);
            }
            catch (Exception ex)
            {
                // Registra el error en el log con detalles en formato JSON
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener detalles de la contratación con ID {contratacionId}. Detalles: {errorJson}");

                // Retorna una vista de error
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

                // Cargar las tarjetas guardadas del usuario
                var tarjetasGuardadas = await _contratacionService.ObtenerTarjetasGuardadasDelUsuario(usuarioId);
                viewModel.TarjetasGuardadas = _mapper.Map<List<TarjetaViewModel>>(tarjetasGuardadas);

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
        public async Task<IActionResult> Crear(
    ContratacionViewModel contratacionViewModel,
    int? planId,
    List<int> serviciosAdicionalesSeleccionados,
    List<int> serviciosSeleccionados, // Asegúrate de que este parámetro coincide con el "name" en la vista
    int? tarjetaId)
        {
            try
            {
                // Validar si el modelo es válido
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("El formulario de creación no es válido.");
                    // Preparar el ViewModel para la vista nuevamente
                    var viewModel = await CrearContratacionViewModel();
                    viewModel.PlanId = planId;
                    viewModel.ServiciosAdicionalesSeleccionados = serviciosAdicionalesSeleccionados ?? new List<int>();
                    viewModel.ServiciosSeleccionados = serviciosSeleccionados ?? new List<int>();
                    return View(viewModel);
                }

                // Depuración: Verificar que llegan los datos correctos
                _logger.LogInformation("PlanId: {PlanId}", planId);
                _logger.LogInformation("ServiciosSeleccionados: {ServiciosSeleccionados}",
                    serviciosSeleccionados != null ? string.Join(", ", serviciosSeleccionados) : "Ninguno");
                _logger.LogInformation("ServiciosAdicionalesSeleccionados: {ServiciosAdicionalesSeleccionados}",
                    serviciosAdicionalesSeleccionados != null ? string.Join(", ", serviciosAdicionalesSeleccionados) : "Ninguno");
                _logger.LogInformation("TarjetaId: {TarjetaId}", tarjetaId);

                // Crear entidad Contratacion
                var contratacion = _mapper.Map<Contratacion>(contratacionViewModel);
                contratacion.Estado = "Activa";

                // Guardar la contratación antes de asociar servicios
                var resultadoContratacion = await _contratacionService.AgregarContratacion(contratacion);
                if (!resultadoContratacion)
                {
                    _logger.LogWarning("No se pudo guardar la contratación.");
                    return View("Error");
                }

                _logger.LogInformation("Contratación creada con ID: {ContratacionId}", contratacion.ContratacionId);

                // Asignar Plan o validar servicios seleccionados
                if (planId.HasValue)
                {
                    var plan = await _contratacionService.ObtenerPlanPorId(planId.Value);
                    if (plan == null)
                    {
                        _logger.LogWarning($"El plan con ID {planId} no fue encontrado.");
                        return View("Error");
                    }
                    contratacion.PlanId = plan.PlanId;
                }
                else if (serviciosSeleccionados != null && serviciosSeleccionados.Any())
                {
                    // Registrar servicios independientes como servicios contratados
                    foreach (var servicioId in serviciosSeleccionados)
                    {
                        var servicio = await _contratacionService.ObtenerServicioPorId(servicioId);
                        if (servicio == null)
                        {
                            _logger.LogWarning($"El servicio independiente con ID {servicioId} no fue encontrado. Ignorando.");
                            continue;
                        }

                        var servicioContratado = new ServicioContratado
                        {
                            ContratacionId = contratacion.ContratacionId,
                            ServicioId = servicio.ServicioId
                        };

                        var resultadoServicio = await _contratacionService.AgregarServicioContratado(servicioContratado);
                        if (!resultadoServicio)
                        {
                            _logger.LogWarning($"No se pudo agregar el servicio independiente con ID {servicioId}.");
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("No se seleccionó un plan ni servicios independientes.");
                    ModelState.AddModelError(string.Empty, "Debe seleccionar al menos un plan o servicios independientes.");
                    return View(await CrearContratacionViewModel());
                }

                // Asociar servicios adicionales seleccionados
                if (serviciosAdicionalesSeleccionados != null && serviciosAdicionalesSeleccionados.Any())
                {
                    foreach (var servicioAdicionalId in serviciosAdicionalesSeleccionados)
                    {
                        var servicioAdicional = await _contratacionService.ObtenerServicioAdicionalPorId(servicioAdicionalId);
                        if (servicioAdicional == null)
                        {
                            _logger.LogWarning($"El servicio adicional con ID {servicioAdicionalId} no fue encontrado.");
                            continue;
                        }

                        var servicioAdicionalContratado = new ServicioAdicionalContratado
                        {
                            ContratacionId = contratacion.ContratacionId,
                            ServicioAdicionalId = servicioAdicional.ServicioId,
                            DescuentoAplicado = servicioAdicional.Descuento
                        };

                        var resultadoServicioAdicional = await _contratacionService.AgregarServicioAdicionalContratado(servicioAdicionalContratado);
                        if (!resultadoServicioAdicional)
                        {
                            _logger.LogWarning($"No se pudo asociar el servicio adicional con ID {servicioAdicionalId}.");
                        }
                    }
                }

                // Registrar el pago
                if (!tarjetaId.HasValue)
                {
                    _logger.LogWarning("No se seleccionó ninguna tarjeta guardada.");
                    return View("Error");
                }

                var usuarioId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var montoPago = planId.HasValue
                    ? (await _contratacionService.ObtenerPlanPorId(planId.Value))?.Precio ?? 0
                    : 0;

                var pago = new Pago
                {
                    UsuarioId = usuarioId,
                    ContratacionId = contratacion.ContratacionId,
                    PlanId = planId,
                    TarjetaId = tarjetaId.Value,
                    Monto = montoPago,
                    MetodoPago = "Tarjeta Guardada",
                    Estado = "Pagado",
                    Referencia = Guid.NewGuid().ToString()
                };

                var resultadoPago = await _contratacionService.RegistrarPago(pago);
                if (!resultadoPago)
                {
                    _logger.LogWarning("No se pudo registrar el pago.");
                    return View("Error");
                }

                _logger.LogInformation($"Contratación creada exitosamente con ID {contratacion.ContratacionId}.");
                return RedirectToAction("Detalles", new { contratacionId = contratacion.ContratacionId });
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

                // Crear lista de servicios seleccionados
                var serviciosIds = new List<int>();
                if (servicioId.HasValue)
                {
                    serviciosIds.Add(servicioId.Value); // Agregar el servicioId como lista
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
                var resultado = await _contratacionService.SeleccionarPlanOServicio(contratacionId, planId, serviciosIds, serviciosAdicionalesValidos);

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
