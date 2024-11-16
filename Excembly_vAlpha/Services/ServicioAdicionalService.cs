using System.Collections.Generic;
using System.Linq;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class ServicioAdicionalService
    {
        private readonly ExcemblyDbContext _context;

        public ServicioAdicionalService(ExcemblyDbContext context)
        {
            _context = context;
        }



        // Obtener los servicios adicionales por el ID del Plan
        public async Task<List<ServicioAdicional>> ObtenerServiciosAdicionalesPorPlanIdAsync(int planId)
        {
            return await _context.ServicioAdicional
                .Where(sa => sa.PlanId == planId)
                .Include(sa => sa.Servicio)
                .ToListAsync();
        }

        public async Task<List<ServicioAdicional>> ObtenerServiciosAdicionalesAsync()
        {
            return await _context.ServiciosAdicionales.ToListAsync();
        }



        // Obtener los servicios adicionales por sus IDs
        public async Task<List<ServicioAdicional>> ObtenerServiciosAdicionalesPorIdsAsync(List<int> servicioAdicionalIds)
        {
            return await _context.ServicioAdicional
                .Where(sa => servicioAdicionalIds.Contains(sa.Id))
                .Include(sa => sa.Servicio)
                .ToListAsync();
        }





    }
}
