using System.Collections.Generic;
using System.Linq;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public class ServiciosService
    {
        private readonly ExcemblyDbContext _context;

        public ServiciosService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Método para obtener todos los servicios con sus relaciones pero en el ViewModel
        public IEnumerable<ServicioViewModel> ObtenerTodosLosServicios()
        {
            return _context.Set<Servicio>()
                           .Select(s => new ServicioViewModel
                           {
                               ServicioId = s.ServicioId,
                               Nombre = s.Nombre,
                               Descripcion = s.Descripcion,
                               Precio = s.Precio,
                               ImagenUrl = s.Imagen
                           }).ToList();
        }

        // Método para obtener todos los servicios con sus relaciones
        public IEnumerable<Servicio> ObtenerTodosLosServiciosM()
        {
            return _context.Set<Servicio>()
                           .Include(s => s.TipoServicio)
                           .Include(s => s.PlanServicios)
                           .ThenInclude(ps => ps.Plan)
                           .Include(s => s.ServiciosAdicionales)
                           .ThenInclude(sa => sa.Plan)
                           .ToList();
        }

        // Método para obtener un servicio por ID con sus relaciones
        public Servicio ObtenerServicioPorId(int servicioId)
        {
            return _context.Set<Servicio>()
                           .Include(s => s.TipoServicio)
                           .Include(s => s.PlanServicios)
                           .ThenInclude(ps => ps.Plan)
                           .Include(s => s.ServiciosAdicionales)
                           .ThenInclude(sa => sa.Plan)
                           .FirstOrDefault(s => s.ServicioId == servicioId);
        }

        // Método para obtener servicios individuales por el ID del plan
        public async Task<IEnumerable<ServicioViewModel>> ObtenerServiciosIndividualesPorPlanIdAsync(int planId)
        {
            var servicios = await _context.Set<Servicio>()
                                           .Where(s => s.PlanServicios.Any(ps => ps.PlanId == planId)) // Filtramos por PlanId
                                           .Select(s => new ServicioViewModel
                                           {
                                               ServicioId = s.ServicioId,
                                               Nombre = s.Nombre,
                                               Descripcion = s.Descripcion,
                                               Precio = s.Precio,
                                               ImagenUrl = s.Imagen
                                           })
                                           .ToListAsync();

            return servicios;
        }

        public async Task<List<Servicio>> ObtenerServiciosIndividualesAsync()
        {
            var servicios = await _context.Servicios.ToListAsync();
            return servicios.Where(s => s.EsIndividual).ToList(); // Filtrado en memoria
        }

        // Método para recuperar el ID de un servicio para pasarlo a otra vista
        public int RecuperarServicioId(int servicioId)
        {
            var servicio = _context.Set<Servicio>().Find(servicioId);
            return servicio != null ? servicio.ServicioId : 0;
        }
    }
}
