using System.Collections.Generic;
using System.Linq;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class PlanesService
    {
        private readonly ExcemblyDbContext _context;

        public PlanesService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los planes
        public IEnumerable<Plan> ObtenerTodosLosPlanes()
        {
            return _context.Set<Plan>()
                           .Include(p => p.PlanServicios)
                               .ThenInclude(ps => ps.Servicio)
                                   .ThenInclude(s => s.TipoServicio)
                           .Include(p => p.ServiciosAdicionales)
                               .ThenInclude(sa => sa.Servicio)
                           .Include(p => p.DispositivosPlanFamiliar)
                           .ToList();
        }

        // Método para obtener un plan por ID
        public Plan ObtenerPlanPorId(int planId)
        {
            var plan = _context.Set<Plan>()
                               .Include(p => p.PlanServicios)
                                   .ThenInclude(ps => ps.Servicio)
                               .Include(p => p.ServiciosAdicionales)
                                   .ThenInclude(sa => sa.Servicio)
                               .Include(p => p.DispositivosPlanFamiliar)
                               .FirstOrDefault(p => p.PlanId == planId);

            if (plan != null)
            {
                // Recupera servicios adicionales con su descuento aplicado
                foreach (var servicioAdicional in plan.ServiciosAdicionales)
                {
                    servicioAdicional.Servicio.Precio -= servicioAdicional.Servicio.Precio * servicioAdicional.Descuento;
                }
            }

            return plan;
        }

        // Método para recuperar el ID de un plan para pasarlo a otra vista
        public int RecuperarPlanId(int planId)
        {
            var plan = _context.Set<Plan>().Find(planId);
            return plan != null ? plan.PlanId : 0;
        }

        // Método para obtener todos los IDs de los servicios adicionales asociados a los planes
        public List<int> ObtenerTodosLosServiciosAdicionalesIds()
        {
            return _context.Set<ServicioAdicional>()
                           .Select(sa => sa.ServicioId)
                           .Distinct()
                           .ToList();
        }

        // Método para obtener un servicio adicional por su ID
        public Servicio ObtenerServicioAdicionalPorId(int servicioId)
        {
            return _context.Set<Servicio>()
                           .Include(s => s.ServiciosAdicionales)
                           .FirstOrDefault(s => s.ServicioId == servicioId);
        }

        // Método para recuperar solo el ID de un servicio adicional específico
        public int RecuperarServicioAdicionalId(int servicioId)
        {
            var servicioAdicional = _context.Set<ServicioAdicional>().Find(servicioId);
            return servicioAdicional != null ? servicioAdicional.ServicioId : 0;
        }
    }
}
