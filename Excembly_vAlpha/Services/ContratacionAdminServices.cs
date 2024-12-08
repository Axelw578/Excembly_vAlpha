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
                    .Include(c => c.Usuario) // Incluimos la relación Usuario para obtener sus datos
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .Include(c => c.ServiciosAdicionalesContratados) // Incluye servicios adicionales
                        .ThenInclude(sac => sac.ServicioAdicional)
                    .ToListAsync();

                // Mapeo manual adicional para incluir el NombreUsuario y ServiciosAdicionalesContratados
                var contratacionViewModels = contrataciones.Select(c => new ContratacionAdminViewModel
                {
                    ContratacionId = c.ContratacionId,
                    FechaContratacion = c.FechaContratacion,
                    Estado = c.Estado,
                    TipoServicio = c.TipoServicio,
                    UsuarioId = c.UsuarioId,
                    NombreUsuario = c.Usuario?.Nombre ?? "Usuario desconocido",
                    ApellidoUsuario = c.Usuario?.Apellidos ?? "",
                    CorreoUsuario = c.Usuario?.CorreoElectronico ?? "",
                    TelefonoUsuario = c.Usuario?.Telefono ?? "",
                    PlanContratado = c.Plan?.Nombre ?? "Sin plan",
                    ServicioContratado = c.Servicio?.Nombre ?? "Sin servicio",
                    ServiciosAdicionalesContratados = c.ServiciosAdicionalesContratados
                        .Select(sac => sac.ServicioAdicional?.Nombre ?? "Servicio adicional desconocido")
                        .ToList() // Aquí usamos la propiedad correcta
                });

                return contratacionViewModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }


        public async Task<bool> MarcarComoCompletadoAsync(int contratacionId)
        {
            var contratacion = await _context.Contratacion.FindAsync(contratacionId);

            if (contratacion == null)
            {
                return false;
            }

            contratacion.Estado = "Completado";
            await _context.SaveChangesAsync();
            return true;
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
                    .Include(c => c.ServiciosAdicionalesContratados) // Incluye servicios adicionales
                        .ThenInclude(sac => sac.ServicioAdicional)
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
                // Recuperar la contratación con las relaciones necesarias
                var contratacion = await _context.Contratacion
                    .Include(c => c.Servicio)
                    .Include(c => c.ServiciosAdicionalesContratados)
                    .FirstOrDefaultAsync(c => c.ContratacionId == contratacionId);

                if (contratacion == null)
                    throw new KeyNotFoundException($"Contratación con ID {contratacionId} no encontrada.");

                // Recuperar el técnico
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
                    ServicioId = contratacion.ServicioId,
                    FechaAsignacion = DateTime.Now,
                    Estado = "Asignado"
                };

                // Agregar la asignación
                _context.AsignacionesTecnicos.Add(nuevaAsignacion);

                // Actualizar la disponibilidad del técnico
                tecnico.Disponibilidad = false;

                // Guardar los cambios en la base de datos
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
        public async Task<IEnumerable<ContratacionAdminViewModel>> FiltrarContratacionesActivasAsync(DateTime? fechaInicio, DateTime? fechaFin, int? usuarioId)
        {
            try
            {
                // Base: solo contrataciones activas y activas
                var query = _context.Contratacion
                    .Include(c => c.Usuario)
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .Include(c => c.ServiciosAdicionalesContratados)
                        .ThenInclude(sac => sac.ServicioAdicional)
                    .Where(c => c.Estado == "Activo" || c.Estado == "Activa") // Incluir "Activo" y "Activa"
                    .AsQueryable();

                // Filtros adicionales
                if (fechaInicio.HasValue)
                    query = query.Where(c => c.FechaContratacion >= fechaInicio.Value);

                if (fechaFin.HasValue)
                    query = query.Where(c => c.FechaContratacion <= fechaFin.Value);

                if (usuarioId.HasValue)
                    query = query.Where(c => c.UsuarioId == usuarioId.Value);

                var contrataciones = await query.ToListAsync();

                // Mapear los resultados al ViewModel
                return contrataciones.Select(c => new ContratacionAdminViewModel
                {
                    ContratacionId = c.ContratacionId,
                    FechaContratacion = c.FechaContratacion,
                    Estado = c.Estado,
                    TipoServicio = c.TipoServicio,
                    UsuarioId = c.UsuarioId,
                    NombreUsuario = c.Usuario?.Nombre ?? "Usuario desconocido",
                    ApellidoUsuario = c.Usuario?.Apellidos ?? "",
                    CorreoUsuario = c.Usuario?.CorreoElectronico ?? "",
                    TelefonoUsuario = c.Usuario?.Telefono ?? "",
                    PlanContratado = c.Plan?.Nombre ?? "Sin plan",
                    ServicioContratado = c.Servicio?.Nombre ?? "Sin servicio",
                    ServiciosAdicionalesContratados = c.ServiciosAdicionalesContratados
                        .Select(sac => sac.ServicioAdicional?.Nombre ?? "Servicio adicional desconocido")
                        .ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        public async Task<IEnumerable<ContratacionAdminViewModel>> ObtenerContratacionesActivasAsync()
        {
            try
            {
                // Filtrar contrataciones activas y activas
                var contrataciones = await _context.Contratacion
                    .Include(c => c.Usuario)
                    .Include(c => c.Plan)
                    .Include(c => c.Servicio)
                    .Include(c => c.ServiciosAdicionalesContratados)
                        .ThenInclude(sac => sac.ServicioAdicional)
                    .Where(c => c.Estado == "Activo" || c.Estado == "Activa") // Incluir "Activo" y "Activa"
                    .ToListAsync();

                // Mapear al modelo de vista
                return contrataciones.Select(c => new ContratacionAdminViewModel
                {
                    ContratacionId = c.ContratacionId,
                    FechaContratacion = c.FechaContratacion,
                    Estado = c.Estado,
                    TipoServicio = c.TipoServicio,
                    UsuarioId = c.UsuarioId,
                    NombreUsuario = c.Usuario?.Nombre ?? "Usuario desconocido",
                    ApellidoUsuario = c.Usuario?.Apellidos ?? "",
                    CorreoUsuario = c.Usuario?.CorreoElectronico ?? "",
                    TelefonoUsuario = c.Usuario?.Telefono ?? "",
                    PlanContratado = c.Plan?.Nombre ?? "Sin plan",
                    ServicioContratado = c.Servicio?.Nombre ?? "Sin servicio",
                    ServiciosAdicionalesContratados = c.ServiciosAdicionalesContratados
                        .Select(sac => sac.ServicioAdicional?.Nombre ?? "Servicio adicional desconocido")
                        .ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
