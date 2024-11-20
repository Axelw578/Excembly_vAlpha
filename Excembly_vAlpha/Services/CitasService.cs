using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class CitasService
    {
        private readonly ExcemblyDbContext _context;

        public CitasService(ExcemblyDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, Cita Cita, string Message)> ObtenerCitaPorIdAsync(int citaId)
        {
            try
            {
                var cita = await _context.Citas
                    .Include(c => c.Tecnico)
                    .Include(c => c.Direccion)
                    .Include(c => c.Plan)
                    .Include(c => c.ServiciosAdicionales)
                    .FirstOrDefaultAsync(c => c.CitaId == citaId);

                if (cita == null)
                {
                    return (false, null, "Cita no encontrada.");
                }

                return (true, cita, "Cita obtenida exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al obtener la cita: {ex.Message}");
            }
        }


        // Crear una nueva cita
        public async Task<(bool Success, string Message, int? CitaId)> CrearCitaAsync(Cita nuevaCita)
        {
            try
            {
                await _context.Citas.AddAsync(nuevaCita);
                await _context.SaveChangesAsync();
                return (true, "Cita creada exitosamente.", nuevaCita.CitaId);
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear la cita: {ex.Message}", null);
            }
        }

        // Obtener citas por usuario
        public async Task<(bool Success, List<Cita> Citas, string Message)> ObtenerCitasPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var citas = await _context.Citas
                    .Where(c => c.UsuarioId == usuarioId)
                    .Include(c => c.Tecnico)
                    .Include(c => c.Direccion)
                    .Include(c => c.Plan)
                    .Include(c => c.ServiciosAdicionales)
                    .Include(c => c.PlanesPersonalizados)
                    .ToListAsync();

                return (true, citas, "Citas obtenidas exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al obtener las citas: {ex.Message}");
            }
        }

        // Editar cita
        public async Task<(bool Success, string Message)> EditarCitaAsync(Cita citaActualizada)
        {
            try
            {
                var citaExistente = await _context.Citas.FindAsync(citaActualizada.CitaId);

                if (citaExistente == null)
                {
                    return (false, "La cita no existe.");
                }

                citaExistente.FechaCita = citaActualizada.FechaCita;
                citaExistente.DireccionId = citaActualizada.DireccionId;
                citaExistente.TecnicoId = citaActualizada.TecnicoId;
                citaExistente.Comentarios = citaActualizada.Comentarios;
                citaExistente.FechaCitaModificada = DateTime.Now;

                _context.Citas.Update(citaExistente);
                await _context.SaveChangesAsync();

                return (true, "Cita actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al editar la cita: {ex.Message}");
            }
        }

        // Cancelar cita
        public async Task<(bool Success, string Message)> CancelarCitaAsync(int citaId, string motivo)
        {
            try
            {
                var cita = await _context.Citas.FindAsync(citaId);

                if (cita == null)
                {
                    return (false, "La cita no existe.");
                }

                cita.EstadoCita = "Cancelada";
                cita.FechaCancelacion = DateTime.Now;
                cita.MotivoCancelacion = motivo;

                _context.Citas.Update(cita);
                await _context.SaveChangesAsync();

                return (true, "Cita cancelada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al cancelar la cita: {ex.Message}");
            }
        }

        // Obtener citas disponibles para un técnico
        public async Task<(bool Success, List<Cita> Citas, string Message)> ObtenerCitasPorTecnicoAsync(int tecnicoId, string estado = "Programada")
        {
            try
            {
                var citas = await _context.Citas
                    .Where(c => c.TecnicoId == tecnicoId && c.EstadoCita == estado)
                    .Include(c => c.Usuario)
                    .Include(c => c.Direccion)
                    .Include(c => c.Plan)
                    .Include(c => c.ServiciosAdicionales)
                    .ToListAsync();

                return (true, citas, "Citas obtenidas exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al obtener las citas: {ex.Message}");
            }
        }
    }
}
