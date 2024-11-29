using AutoMapper;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public class ContratacionAdminServices : IContratacionAdminServices
    {
        private readonly ExcemblyDbContext _context;
        private readonly IMapper _mapper;

        public ContratacionAdminServices(ExcemblyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 1. Obtener todas las contrataciones
        public async Task<IEnumerable<ContratacionAdminViewModel>> ObtenerTodasContratacionesAsync()
        {
            try
            {
                var contrataciones = await _context.Contratacion
                    .Include(c => c.Usuario)
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ContratacionAdminViewModel>>(contrataciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        // 2. Filtrar contrataciones
        public async Task<IEnumerable<ContratacionAdminViewModel>> FiltrarContratacionesAsync(DateTime? fechaInicio, DateTime? fechaFin, int? usuarioId = null)
        {
            try
            {
                var query = _context.Contratacion
                    .Include(c => c.Usuario)
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .AsQueryable();

                if (fechaInicio.HasValue)
                    query = query.Where(c => c.FechaContratacion >= fechaInicio.Value);

                if (fechaFin.HasValue)
                    query = query.Where(c => c.FechaContratacion <= fechaFin.Value);

                if (usuarioId.HasValue)
                    query = query.Where(c => c.UsuarioId == usuarioId.Value);

                var contrataciones = await query.ToListAsync();
                return _mapper.Map<IEnumerable<ContratacionAdminViewModel>>(contrataciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        // 3. Obtener detalles de una contratación
        public async Task<ContratacionAdminViewModel> ObtenerDetalleContratacionAsync(int contratacionId)
        {
            try
            {
                var contratacion = await _context.Contratacion
                    .Include(c => c.Usuario)
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .FirstOrDefaultAsync(c => c.ContratacionId == contratacionId);

                if (contratacion == null)
                    throw new KeyNotFoundException($"Contratación con ID {contratacionId} no encontrada.");

                return _mapper.Map<ContratacionAdminViewModel>(contratacion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        // 4. Obtener pagos relacionados con una contratación
        public async Task<IEnumerable<PagoAdminViewModel>> ObtenerPagosPorContratacionAsync(int contratacionId)
        {
            try
            {
                var pagos = await _context.Pagos
                    .Include(p => p.Usuario)
                    .Where(p => p.ContratacionId == contratacionId)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<PagoAdminViewModel>>(pagos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        // 5. Asignar técnico a una contratación
        public async Task<bool> AsignarTecnicoAsync(int contratacionId, int tecnicoId)
        {
            try
            {
                var contratacion = await _context.Contratacion.FindAsync(contratacionId);
                if (contratacion == null)
                    throw new KeyNotFoundException($"Contratación con ID {contratacionId} no encontrada.");

                var tecnico = await _context.Tecnicos.FindAsync(tecnicoId);
                if (tecnico == null)
                    throw new KeyNotFoundException($"Técnico con ID {tecnicoId} no encontrado.");

                if (!tecnico.Disponibilidad)
                    throw new InvalidOperationException($"El técnico con ID {tecnicoId} no está disponible.");

                // Crear una nueva asignación
                var nuevaAsignacion = new AsignacionTecnico
                {
                    TecnicoId = tecnicoId,
                    ContratacionId = contratacionId,
                    UsuarioId = contratacion.UsuarioId,
                    FechaAsignacion = DateTime.Now,
                    Estado = "Asignado"
                };

                _context.AsignacionesTecnicos.Add(nuevaAsignacion);

                // Actualizar el estado del técnico y guardar cambios
                tecnico.Disponibilidad = false;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        public async Task<IEnumerable<Tecnico>> ObtenerTecnicosAsync()
        {
            try
            {
                return await _context.Tecnicos.Where(t => t.Disponibilidad == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }


    }
}
