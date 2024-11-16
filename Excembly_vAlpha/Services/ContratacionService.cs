using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class ContratacionService
    {
        private readonly ExcemblyDbContext _context;

        public ContratacionService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Método para crear una nueva contratación con servicios adicionales si los hay
        // Método para crear una nueva contratación con servicios adicionales si los hay
        public async Task<(bool Success, string Message)> CrearContratacionAsync(Contratacion contratacion, List<int> serviciosAdicionalesIds = null)
        {
            try
            {
                // Si hay servicios adicionales seleccionados
                if (serviciosAdicionalesIds != null && serviciosAdicionalesIds.Any())
                {
                    // Obtén los servicios adicionales que coinciden con los IDs proporcionados
                    var serviciosAdicionales = await _context.ServicioAdicional
                        .Where(sa => serviciosAdicionalesIds.Contains(sa.Id)) // Asegúrate de que se usa el campo correcto (Id)
                        .ToListAsync();

                    // Para cada servicio adicional, se agrega a la contratación como un ServicioAdicionalContratado
                    foreach (var servicioAdicional in serviciosAdicionales)
                    {
                        contratacion.ServiciosAdicionalesContratados.Add(new ServicioAdicionalContratado
                        {
                            ServicioAdicional = servicioAdicional, // Asocia el servicio adicional
                            Contratacion = contratacion // Asocia la contratación
                                                        // Aquí puedes agregar el descuento si lo necesitas
                        });
                    }
                }

                // Ahora guarda la contratación con los servicios adicionales asociados
                await _context.Contratacion.AddAsync(contratacion);
                await _context.SaveChangesAsync();

                return (true, "Contratación creada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear la contratación: {ex.Message}");
            }
        }


        // Método para obtener las contrataciones activas de un usuario con detalles adicionales
        public async Task<(bool Success, List<Contratacion> Contrataciones, string Message)> ObtenerContratacionesPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var contrataciones = await _context.Contratacion
                    .Where(c => c.UsuarioId == usuarioId && c.Estado == "Activa")
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .Include(c => c.ServiciosAdicionalesContratados)
                    .Include(c => c.Cita) // Detalles de la cita si es servicio a domicilio
                    .ThenInclude(c => c.Tecnico) // Técnico asignado
                    .ToListAsync();

                return (true, contrataciones, "Contrataciones obtenidas exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al obtener las contrataciones: {ex.Message}");
            }
        }

        // Método para obtener los detalles completos de una contratación, con servicios y técnico asignado
        public async Task<(bool Success, Contratacion Contratacion, string Message)> ObtenerDetalleContratacionAsync(int contratacionId)
        {
            try
            {
                var contratacion = await _context.Contratacion
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .Include(c => c.PlanPersonalizado)
                    .Include(c => c.ServiciosAdicionalesContratados)
                    .Include(c => c.Cita)
                    .ThenInclude(c => c.Tecnico) // Incluye técnico si es servicio a domicilio
                    .FirstOrDefaultAsync(c => c.ContratacionId == contratacionId);

                if (contratacion == null)
                {
                    return (false, null, "La contratación no existe.");
                }

                return (true, contratacion, "Detalle de contratación obtenido exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al obtener el detalle de la contratación: {ex.Message}");
            }
        }

        // Método para editar una contratación existente
        public async Task<(bool Success, string Message)> EditarContratacionAsync(Contratacion contratacion)
        {
            try
            {
                var contratacionExistente = await _context.Contratacion
                    .FirstOrDefaultAsync(c => c.ContratacionId == contratacion.ContratacionId);

                if (contratacionExistente == null)
                {
                    return (false, "La contratación no existe.");
                }

                // Actualizamos la contratación existente con los nuevos valores
                contratacionExistente.PlanId = contratacion.PlanId;
                contratacionExistente.ServicioId = contratacion.ServicioId;
                contratacionExistente.Estado = contratacion.Estado;
                contratacionExistente.FechaContratacion = contratacion.FechaContratacion;
                contratacionExistente.Comentarios = contratacion.Comentarios;

                // Guardamos los cambios
                _context.Contratacion.Update(contratacionExistente);
                await _context.SaveChangesAsync();

                return (true, "Contratación editada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al editar la contratación: {ex.Message}");
            }
        }

        // Método para cancelar una contratación
        public async Task<(bool Success, string Message)> CancelarContratacionAsync(int contratacionId)
        {
            try
            {
                var contratacion = await _context.Contratacion.FindAsync(contratacionId);

                if (contratacion == null)
                {
                    return (false, "La contratación no existe.");
                }

                contratacion.Estado = "Cancelada";
                contratacion.FechaCancelacion = DateTime.Now;

                _context.Contratacion.Update(contratacion);
                await _context.SaveChangesAsync();

                return (true, "Contratación cancelada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al cancelar la contratación: {ex.Message}");
            }
        }
    }
}
