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
                var contratacion = await ObtenerContratacionPorId(contratacionId);
                if (contratacion == null) return false;

                // Si se seleccionó un plan, se asigna el PlanId en la contratación
                if (planId.HasValue)
                {
                    contratacion.PlanId = planId;

                    // Obtener los servicios adicionales con descuento disponibles para este plan
                    var serviciosAdicionales = await _context.ServicioAdicional
                        .Where(sa => sa.PlanId == planId)
                        .ToListAsync();

                    // Asociar los servicios adicionales con descuento a la contratación
                    foreach (var servicioAdicional in serviciosAdicionales)
                    {
                        contratacion.ServiciosAdicionales.Add(servicioAdicional.ServicioId); // colección para servicios adicionales
                    }
                }

                // Si se seleccionó un servicio sin un plan, asignamos solo el servicio individual (sin descuento)
                if (servicioId.HasValue)
                {
                    contratacion.ServicioId = servicioId.Value;
                }

                // Si se especificaron servicios adicionales (opcionalmente seleccionados)
                if (serviciosAdicionalesIds != null)
                {
                    foreach (var servicioAdicionalId in serviciosAdicionalesIds)
                    {
                        var servicioAdicional = await _context.ServicioAdicional
                            .FirstOrDefaultAsync(sa => sa.Id == servicioAdicionalId);
                        if (servicioAdicional != null)
                        {
                            // Asociar el servicio adicional con descuento a la contratación
                            contratacion.ServiciosAdicionales.Add(servicioAdicionalId);
                        }
                    }
                }

                // Actualizar la contratación en el contexto de la base de datos
                _context.Contratacion.Update(contratacion);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Plan o servicio actualizado en la contratación con ID: {contratacion.ContratacionId}");
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

        public async Task<ServicioAdicional> ObtenerServicioAdicionalPorId(int servicioAdicionalId)
        {
            try
            {
                var servicioAdicional = await _context.ServiciosAdicionales.FindAsync(servicioAdicionalId);
                if (servicioAdicional == null)
                {
                    _logger.LogWarning($"No se encontró el servicio adicional con ID: {servicioAdicionalId}");
                }
                return servicioAdicional;
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(ex, Formatting.Indented);
                _logger.LogError($"Error al obtener el servicio adicional con ID {servicioAdicionalId}. Detalles: {errorJson}");
                throw;
            }
        }


    }
}
