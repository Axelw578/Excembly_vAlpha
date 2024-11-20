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
        public async Task<(bool Success, string Message)> CrearContratacionAsync(Contratacion contratacion)
        {
            try
            {
                _context.Contratacion.Add(contratacion);
                await _context.SaveChangesAsync();
                return (true, "Contratación creada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al guardar la contratación: {ex.Message}");
            }
        }



        // Método para validar los datos del modelo de contratación
        private bool ValidarContratacion(Contratacion contratacion)
        {
            // Aquí validamos que los campos obligatorios estén completos
            if (contratacion.PlanId <= 0 || contratacion.ServicioId <= 0)
            {
                return false;
            }

            // Agregar más validaciones según lo necesario
            return true;
        }




        // Método para obtener las contrataciones activas de un usuario con detalles adicionales
        public async Task<(bool Success, List<Contratacion> Contrataciones, string Message, int TotalContrataciones)> ObtenerContratacionesPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var contrataciones = await _context.Contratacion
                    .Where(c => c.UsuarioId == usuarioId && c.Estado == "Activa")
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .Include(c => c.ServiciosAdicionalesContratados)
                    .Include(c => c.Cita)
                    .ThenInclude(c => c.Tecnico)
                    .ToListAsync();

                // Aquí calculas el total de contrataciones
                int totalContrataciones = contrataciones.Count;

                return (true, contrataciones, "Contrataciones obtenidas exitosamente.", totalContrataciones);
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al obtener las contrataciones: {ex.Message}", 0);
            }
        }


        // Método para obtener los detalles completos de una contratación, incluyendo el tipo de servicio
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
                    .ThenInclude(c => c.Tecnico)
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

        // Método para editar una contratación existente, incluyendo TipoServicio
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
                contratacionExistente.TipoServicio = contratacion.TipoServicio; // Actualización del TipoServicio
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
