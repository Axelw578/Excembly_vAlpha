using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;  // Para serializar objetos de error en JSON
using AutoMapper;      // Para AutoMapper
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Services
{
    public class ContratacionService : IContratacionService
    {
        private readonly ExcemblyDbContext _context;
        private readonly ILogger<ContratacionService> _logger;
        private readonly IMapper _mapper; // Dependencia de AutoMapper

        public ContratacionService(ExcemblyDbContext context, ILogger<ContratacionService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Contratacion>> ObtenerContratacionesDelUsuario(int usuarioId)
        {
            try
            {
                var contrataciones = await Task.Run(() =>
                    _context.Contratacion.Where(c => c.UsuarioId == usuarioId).ToList());

                if (!contrataciones.Any())
                {
                    _logger.LogWarning($"No se encontraron contrataciones para el usuario con ID: {usuarioId}");
                }

                return contrataciones;
            }
            catch (Exception ex)
            {
                // Serializa el error y lo registra en JSON para facilitar la depuración
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener contrataciones del usuario con ID: {usuarioId}. Detalles: {errorJson}");
                throw;
            }
        }

        public async Task<Contratacion?> ObtenerContratacionPorId(int contratacionId)
        {
            try
            {
                var contratacion = await _context.Contratacion
                    .Include(c => c.ServiciosContratados)
                        .ThenInclude(sc => sc.Servicio)
                    .FirstOrDefaultAsync(c => c.ContratacionId == contratacionId);

                if (contratacion == null)
                {
                    _logger.LogWarning($"No se encontró la contratación con ID: {contratacionId}");
                }

                return contratacion;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener la contratación con ID: {contratacionId}. Detalles: {errorJson}");
                throw;
            }
        }


        public async Task<bool> AgregarContratacion(Contratacion contratacion)
        {
            try
            {
                await _context.Contratacion.AddAsync(contratacion);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Contratación agregada exitosamente con ID: {contratacion.ContratacionId}");
                return true;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al agregar la contratación. Detalles: {errorJson}");
                return false;
            }
        }

        public async Task<bool> EditarContratacion(Contratacion contratacion)
        {
            try
            {
                _context.Contratacion.Update(contratacion);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Contratación actualizada exitosamente con ID: {contratacion.ContratacionId}");
                return true;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al editar la contratación con ID: {contratacion.ContratacionId}. Detalles: {errorJson}");
                return false;
            }
        }

        public async Task<bool> CancelarContratacion(int contratacionId)
        {
            try
            {
                var contratacion = await ObtenerContratacionPorId(contratacionId);
                if (contratacion == null) return false;

                contratacion.Estado = "Cancelada";
                contratacion.FechaCancelacion = DateTime.Now;

                _context.Contratacion.Update(contratacion);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Contratación cancelada exitosamente con ID: {contratacion.ContratacionId}");
                return true;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al cancelar la contratación con ID: {contratacionId}. Detalles: {errorJson}");
                return false;
            }
        }


        public async Task<bool> SeleccionarPlanOServicio(int contratacionId, int? planId, List<int>? serviciosIds, List<int>? serviciosAdicionalesIds)
        {
            try
            {
                // Obtener la contratación existente
                var contratacion = await ObtenerContratacionPorId(contratacionId);
                if (contratacion == null)
                {
                    _logger.LogWarning($"La contratación con ID: {contratacionId} no existe.");
                    return false;
                }

                // Validar que la contratación esté activa
                if (contratacion.Estado != "Activa")
                {
                    _logger.LogWarning($"No se puede modificar una contratación en estado {contratacion.Estado}. ID: {contratacionId}");
                    return false;
                }

                // Validar exclusividad entre plan y servicios independientes
                if (planId.HasValue && serviciosIds != null && serviciosIds.Any())
                {
                    _logger.LogWarning($"No se puede seleccionar un plan y servicios independientes al mismo tiempo. Contratación ID: {contratacionId}");
                    return false;
                }

                // Si se seleccionó un plan
                if (planId.HasValue)
                {
                    contratacion.PlanId = planId.Value;

                    // Eliminar servicios y servicios adicionales anteriores asociados a este plan
                    var serviciosAnteriores = _context.ServicioContratado
                        .Where(sc => sc.ContratacionId == contratacionId);
                    _context.ServicioContratado.RemoveRange(serviciosAnteriores);

                    var serviciosAdicionalesAnteriores = _context.ServicioAdicionalContratado
                        .Where(sac => sac.ContratacionId == contratacionId);
                    _context.ServicioAdicionalContratado.RemoveRange(serviciosAdicionalesAnteriores);
                }

                // Si se seleccionaron servicios independientes
                if (serviciosIds != null && serviciosIds.Any())
                {
                    foreach (var servicioId in serviciosIds.Distinct())
                    {
                        // Verificar que el servicio exista
                        var servicio = await _context.Servicios
                            .FirstOrDefaultAsync(s => s.ServicioId == servicioId);

                        if (servicio != null)
                        {
                            // Verificar si el servicio ya está asociado
                            var servicioExistente = await _context.ServicioContratado
                                .FirstOrDefaultAsync(sc => sc.ContratacionId == contratacionId && sc.ServicioId == servicioId);

                            if (servicioExistente != null)
                            {
                                _logger.LogWarning($"El servicio con ID: {servicioId} ya está asociado a la contratación con ID: {contratacionId}");
                                continue; // Evitar agregar el mismo servicio
                            }

                            // Crear una entrada en ServicioContratado
                            var servicioContratado = new ServicioContratado
                            {
                                ContratacionId = contratacionId,
                                ServicioId = servicio.ServicioId
                            };

                            await _context.ServicioContratado.AddAsync(servicioContratado);
                            _logger.LogInformation($"Servicio con ID: {servicioId} agregado a la contratación con ID: {contratacionId}");
                        }
                        else
                        {
                            _logger.LogWarning($"El servicio con ID: {servicioId} no existe.");
                        }
                    }
                }

                // Asociar servicios adicionales seleccionados manualmente
                if (serviciosAdicionalesIds != null && serviciosAdicionalesIds.Any())
                {
                    foreach (var servicioAdicionalId in serviciosAdicionalesIds.Distinct())
                    {
                        // Verificar que el servicio adicional exista
                        var servicioAdicional = await _context.ServiciosAdicionales
                            .FirstOrDefaultAsync(sa => sa.Id == servicioAdicionalId);

                        if (servicioAdicional != null)
                        {
                            // Verificar si el servicio adicional ya está asociado
                            var servicioAdicionalExistente = await _context.ServicioAdicionalContratado
                                .FirstOrDefaultAsync(sac => sac.ContratacionId == contratacionId && sac.ServicioAdicionalId == servicioAdicionalId);

                            if (servicioAdicionalExistente != null)
                            {
                                _logger.LogWarning($"El servicio adicional con ID: {servicioAdicionalId} ya está asociado a la contratación con ID: {contratacionId}");
                                continue; // Evitar agregar el mismo servicio adicional
                            }

                            // Crear una entrada en ServicioAdicionalContratado
                            var servicioAdicionalContratado = new ServicioAdicionalContratado
                            {
                                ContratacionId = contratacionId,
                                ServicioAdicionalId = servicioAdicional.Id,
                                DescuentoAplicado = servicioAdicional.Descuento
                            };

                            await _context.ServicioAdicionalContratado.AddAsync(servicioAdicionalContratado);
                            _logger.LogInformation($"Servicio adicional contratado con ID: {servicioAdicionalId} agregado a la contratación con ID: {contratacionId}");
                        }
                        else
                        {
                            _logger.LogWarning($"El servicio adicional con ID: {servicioAdicionalId} no existe.");
                        }
                    }
                }

                // Guardar los cambios en la base de datos
                _context.Contratacion.Update(contratacion);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Plan o servicios actualizados exitosamente en la contratación con ID: {contratacionId}");
                return true;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al seleccionar plan o servicios para la contratación con ID: {contratacionId}. Detalles: {errorJson}");
                return false;
            }
        }


        public async Task<ContratacionViewModel?> ObtenerDetallesDeContratacion(int contratacionId)
        {
            try
            {
                // Cargar los detalles básicos de la contratación
                var contratacion = await _context.Contratacion
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .FirstOrDefaultAsync(c => c.ContratacionId == contratacionId);

                if (contratacion == null)
                {
                    _logger.LogWarning($"No se encontró la contratación con ID: {contratacionId}");
                    return null;
                }

                // Mapear contratación al ViewModel
                var contratacionViewModel = _mapper.Map<ContratacionViewModel>(contratacion);

                // Cargar y mapear servicios contratados
                contratacionViewModel.ServiciosContratados = await ObtenerServiciosContratadosPorContratacionId(contratacionId);

                // Cargar y mapear servicios adicionales contratados
                var serviciosAdicionalesContratados = await ObtenerServiciosAdicionalesContratadosPorContratacionId(contratacionId);

                // Transformar cada ServicioAdicionalContratadoViewModel en ServicioAdicionalViewModel
                contratacionViewModel.ServiciosAdicionalesContratados = serviciosAdicionalesContratados
                    .Select(sac => new ServicioAdicionalViewModel
                    {
                        ServicioId = sac.ServicioAdicionalId,
                        NombreServicio = sac.ServicioAdicionalNombre,
                        PrecioOriginal = sac.ServicioAdicionalPrecio,
                        PrecioConDescuento = sac.DescuentoAplicado,
                        Descuento = sac.DescuentoAplicado
                    });

                // Cargar datos adicionales si es necesario
                contratacionViewModel.PlanesDisponibles = _mapper.Map<IEnumerable<PlanViewModel>>(await ObtenerPlanesDisponibles());
                contratacionViewModel.ServiciosDisponibles = _mapper.Map<IEnumerable<ServicioViewModel>>(await ObtenerServiciosDisponibles());
                contratacionViewModel.ServiciosAdicionalesDisponibles = _mapper.Map<IEnumerable<ServicioAdicionalViewModel>>(await ObtenerServiciosAdicionalesDisponibles());

                return contratacionViewModel;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener los detalles de la contratación con ID: {contratacionId}. Detalles: {errorJson}");
                throw;
            }
        }





        // para guardar servicios independientes
        public async Task<bool> AgregarServicioContratado(ServicioContratado servicioContratado)
        {
            try
            {
                await _context.ServicioContratado.AddAsync(servicioContratado);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError(ex, "Error al agregar el servicio contratado.");
                return false;
            }
        }


        //para seleccionar

        public async Task<IEnumerable<Plan>> ObtenerPlanesDisponibles()
        {
            try
            {
                return await _context.Planes.ToListAsync();
            }
            catch (Exception ex)
            {
                // Serializa el error y lo registra en JSON para facilitar la depuración
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener los planes disponibles. Detalles: {errorJson}");
                throw;  // Re-lanza la excepción para que la capa superior también la maneje
            }
        }

        public async Task<Plan> ObtenerPlanPorId(int planId)
        {
            try
            {
                var plan = await _context.Planes.FindAsync(planId);
                if (plan == null)
                {
                    _logger.LogWarning($"No se encontró el plan con ID: {planId}");
                }
                return plan;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener el plan con ID {planId}. Detalles: {errorJson}");
                throw;
            }
        }

        public async Task<IEnumerable<Servicio>> ObtenerServiciosDisponibles()
        {
            try
            {
                return await _context.Servicios.ToListAsync();
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener los servicios disponibles. Detalles: {errorJson}");
                throw;
            }
        }

        public async Task<Servicio> ObtenerServicioPorId(int servicioId)
        {
            try
            {
                var servicio = await _context.Servicios.FindAsync(servicioId);
                if (servicio == null)
                {
                    _logger.LogWarning($"No se encontró el servicio con ID: {servicioId}");
                }
                return servicio;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener el servicio con ID {servicioId}. Detalles: {errorJson}");
                throw;
            }
        }

        public async Task<IEnumerable<ServicioAdicional>> ObtenerServiciosAdicionalesDisponibles()
        {
            try
            {
                return await _context.ServiciosAdicionales.ToListAsync();
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener los servicios adicionales disponibles. Detalles: {errorJson}");
                throw;
            }
        }

        public async Task<ServicioAdicional> ObtenerServicioAdicionalPorId(int id, int? planId = null)
        {
            try
            {
                // Buscar usando el ID único, pero también validar con PlanId si es necesario
                var servicioAdicional = await _context.ServiciosAdicionales
                    .FirstOrDefaultAsync(sa => sa.Id == id && (planId == null || sa.PlanId == planId));

                if (servicioAdicional == null)
                {
                    _logger.LogWarning($"No se encontró el servicio adicional con ID: {id} y PlanId: {planId}");
                }

                return servicioAdicional;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener el servicio adicional con ID {id}. Detalles: {errorJson}");
                throw;
            }
        }


        public async Task<bool> AgregarServicioAdicionalContratado(ServicioAdicionalContratado servicioAdicionalContratado)
        {
            try
            {
                // Agregar el servicio adicional a la base de datos
                await _context.ServicioAdicionalContratado.AddAsync(servicioAdicionalContratado);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Servicio adicional contratado agregado exitosamente: {servicioAdicionalContratado.ServicioAdicionalId} para la contratación {servicioAdicionalContratado.ContratacionId}");
                return true;
            }
            catch (Exception ex)
            {
                // Serializar y registrar cualquier error en el log
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al agregar el servicio adicional contratado: {errorJson}");
                return false;
            }
        }

        public async Task<IEnumerable<TarjetaGuardada>> ObtenerTarjetasGuardadasDelUsuario(int usuarioId)
        {
            // Obtener las tarjetas guardadas del usuario desde la base de datos
            var tarjetas = await Task.FromResult(
                _context.TarjetasGuardadas
                        .Where(t => t.UsuarioId == usuarioId)
                        .ToList()
            );

            return tarjetas;
        }

        public async Task<bool> RegistrarPago(Pago pago)
        {
            try
            {
                _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al registrar el pago: {ex.Message}");
                return false;
            }
        }

        // para dertalles
        public async Task<IEnumerable<ServicioContratadoViewModel>> ObtenerServiciosContratadosPorContratacionId(int contratacionId)
        {
            try
            {
                var serviciosContratados = await _context.ServicioContratado
                    .Where(sc => sc.ContratacionId == contratacionId)
                    .Include(sc => sc.Servicio)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ServicioContratadoViewModel>>(serviciosContratados);
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener los servicios contratados para la contratación ID: {contratacionId}. Detalles: {errorJson}");
                throw;
            }
        }

        public async Task<IEnumerable<ServicioAdicionalContratadoViewModel>> ObtenerServiciosAdicionalesContratadosPorContratacionId(int contratacionId)
        {
            try
            {
                var serviciosAdicionalesContratados = await _context.ServicioAdicionalContratado
                    .Where(sac => sac.ContratacionId == contratacionId)
                    .Include(sac => sac.ServicioAdicional)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ServicioAdicionalContratadoViewModel>>(serviciosAdicionalesContratados);
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener los servicios adicionales contratados para la contratación ID: {contratacionId}. Detalles: {errorJson}");
                throw;
            }
        }




    }
}
