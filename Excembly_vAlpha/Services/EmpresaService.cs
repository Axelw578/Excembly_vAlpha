using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
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
        public async Task<List<Comentario>> ObtenerComentariosUsuarios()
        {
            return await _context.Set<Comentario>()
                .Include(c => c.Usuario)  // Incluye la relación con Usuario
                .Include(c => c.Servicio) // Incluye la relación con Servicio
                .Include(c => c.Plan)     // Incluye la relación con Plan
                .Include(c => c.PlanPersonalizado) // Incluye la relación con PlanPersonalizado
                .Include(c => c.Contratacion) // Incluye la relación con Contratación
                .OrderByDescending(c => c.FechaComentario) // Ordenar por la fecha del comentario (más recientes primero)
                .Take(5) // Tomar los 5 comentarios más recientes
                .ToListAsync();
        }
    }
}
