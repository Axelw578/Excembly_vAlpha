using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public class EmpresaService
    {
        private readonly ExcemblyDbContext _context;

        public EmpresaService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Obtener la información de la empresa
        public async Task<AcercaDe> InformacionEmpresa()
        {
            // Obtenemos el registro de la empresa que contiene misión, visión, mapa y aclaraciones
            return await _context.Set<AcercaDe>().FirstOrDefaultAsync();
        }


        // Obtener comentarios de los usuarios
        public async Task<List<ComentarioViewModel>> ObtenerComentariosUsuarios()
        {
            var comentarios = await _context.Set<Comentario>()
                .Include(c => c.Usuario)          // Incluye la relación con Usuario
                .OrderByDescending(c => c.FechaComentario) // Ordenar por la fecha del comentario (más recientes primero)
                .Take(5)  // Tomar los 5 comentarios más recientes
                .ToListAsync();

            // Mapea los comentarios a ComentarioViewModel
            var comentarioViewModels = comentarios.Select(c => new ComentarioViewModel
            {
                NombreUsuario = c.Usuario.Nombre,           // Nombre del usuario
                Opinion = c.Opinion,                        // Opinión del comentario
                FechaComentario = c.FechaComentario,       // Fecha del comentario
                FotoPerfilUrl = c.Usuario.FotoPerfilUrl,    // Foto de perfil del usuario
                FotoComentarioUrl = c.FotoUrl              // Foto del comentario (opcional)
            }).ToList();

            return comentarioViewModels;
        }


    }
}
