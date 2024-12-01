using AutoMapper;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Excembly_vAlpha.Services
{
    public class ComentarioAdminService : IComentarioAdminService
    {
        private readonly ExcemblyDbContext _context;
        private readonly IMapper _mapper;

        public ComentarioAdminService(ExcemblyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Método para obtener todos los comentarios
        public async Task<List<ComentarioAdminViewModel>> ObtenerTodosComentariosAsync()
        {
            var comentarios = await _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Contratacion)
                    .ThenInclude(co => co.Plan)
                .Include(c => c.Contratacion)
                    .ThenInclude(co => co.Servicio)
                .ToListAsync();

            return _mapper.Map<List<ComentarioAdminViewModel>>(comentarios);
        }

        // Método para obtener comentarios con filtros (ordenados)
        public async Task<List<ComentarioAdminViewModel>> ObtenerComentariosFiltradosAsync(
            string ordenPor = "FechaComentario",
            bool ascendente = true)
        {
            var query = _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Contratacion)
                    .ThenInclude(co => co.Plan)
                .Include(c => c.Contratacion)
                    .ThenInclude(co => co.Servicio)
                .AsQueryable();

            // Orden dinámico
            query = ordenPor switch
            {
                "FechaComentario" => ascendente ? query.OrderBy(c => c.FechaComentario) : query.OrderByDescending(c => c.FechaComentario),
                "Usuario" => ascendente ? query.OrderBy(c => c.Usuario.Nombre) : query.OrderByDescending(c => c.Usuario.Nombre),
                _ => query.OrderBy(c => c.ComentarioId) // Por defecto
            };

            var comentarios = await query.ToListAsync();

            return _mapper.Map<List<ComentarioAdminViewModel>>(comentarios);
        }

        // Método para obtener un comentario por ID
        public async Task<ComentarioAdminViewModel> ObtenerComentarioPorIdAsync(int comentarioId)
        {
            var comentario = await _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Contratacion)
                    .ThenInclude(co => co.Plan)
                .Include(c => c.Contratacion)
                    .ThenInclude(co => co.Servicio)
                .FirstOrDefaultAsync(c => c.ComentarioId == comentarioId);

            if (comentario == null)
                throw new KeyNotFoundException($"No se encontró un comentario con ID {comentarioId}");

            return _mapper.Map<ComentarioAdminViewModel>(comentario);
        }

        // Método para ver los detalles de un comentario
        public async Task<ComentarioDetalleAdminViewModel> ObtenerDetalleComentarioAsync(int comentarioId)
        {
            try
            {
                var comentario = await _context.Comentario
                    .Include(c => c.Usuario)
                    .Include(c => c.Contratacion)
                        .ThenInclude(co => co.Plan)
                    .Include(c => c.Contratacion)
                        .ThenInclude(co => co.Servicio)
                    .Include(c => c.Contratacion)
                        .ThenInclude(co => co.AsignacionesTecnico)
                            .ThenInclude(at => at.Tecnico)
                    .FirstOrDefaultAsync(c => c.ComentarioId == comentarioId);

                if (comentario == null)
                    throw new KeyNotFoundException($"No se encontró un comentario con ID {comentarioId}");

                return _mapper.Map<ComentarioDetalleAdminViewModel>(comentario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(new { Error = ex.Message, StackTrace = ex.StackTrace }));
                throw;
            }
        }
    }
}
