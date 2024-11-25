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
                var contratacion = await Task.Run(() =>
                    _context.Contratacion.FirstOrDefault(c => c.ContratacionId == contratacionId));

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


        public async Task<bool> SeleccionarPlanOServicio(int contratacionId, int? planId, int? servicioId, List<int>? serviciosAdicionalesIds)
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

                // Validar exclusividad entre plan y servicio individual
                if (planId.HasValue && servicioId.HasValue)
                {
                    _logger.LogWarning($"No se puede seleccionar un plan y un servicio individual al mismo tiempo. Contratación ID: {contratacionId}");
                    return false;
                }

                // Si se seleccionó un plan
                if (planId.HasValue)
                {
                    contratacion.PlanId = planId.Value;

                    // Eliminar servicios adicionales anteriores asociados a este plan
                    var serviciosAdicionalesAnteriores = _context.ServicioAdicionalContratado
                        .Where(sac => sac.ContratacionId == contratacionId);
                    _context.ServicioAdicionalContratado.RemoveRange(serviciosAdicionalesAnteriores);
                }

                // Si se seleccionó un servicio individual
                if (servicioId.HasValue)
                {
                    contratacion.ServicioId = servicioId.Value;
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
                                ContratacionId = contratacionId, // Ligar con la contratación
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

                _logger.LogInformation($"Plan o servicio actualizado exitosamente en la contratación con ID: {contratacionId}");
                return true;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al seleccionar plan o servicio para la contratación con ID: {contratacionId}. Detalles: {errorJson}");
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




    }
}
