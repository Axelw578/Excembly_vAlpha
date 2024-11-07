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
        private readonly ExcemblyDbContext _context; // 

        public EmpresaService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // obtener la información de la empresa
        public async Task<AcercaDe> InformacionEmpresa()
        {
            // Obtenemos el registro de la empresa que contiene misión, visión, mapa y aclaraciones
            return await _context.Set<AcercaDe>().FirstOrDefaultAsync();
        }

        // obtener reseñas de usuarios 
        public async Task<List<Cita>> ReseñasUsuarios()
        {
            return await _context.Set<Cita>()
                .Include(c => c.Usuario)
                .Where(c => !string.IsNullOrEmpty(c.Comentarios)) // Filtrar solo citas con comentarios
                .OrderByDescending(c => c.FechaCita)               // Ordenar por fecha de cita
                .Take(5)                                           // Tomar las 5 más recientes
                .ToListAsync();
        }
    }
}
