using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
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
        public async Task<IEnumerable<Plan>> ObtenerTodosLosPlanesAsync()
        {
            return await _context.Planes
                .Include(p => p.PlanServicios)
                    .ThenInclude(ps => ps.Servicio)
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> ObtenerTodosLosPlanesFamiliarAsync()
        {
            return await _context.Set<Plan>()
                .Include(p => p.PlanServicios)  // Cargar la relación PlanServicio
                    .ThenInclude(ps => ps.Servicio)  // Cargar el Servicio relacionado
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .Include(p => p.DispositivosPlanFamiliar)
                .ToListAsync();
        }


        // Método para obtener todos los planes
        public IEnumerable<Plan> ObtenerTodosLosPlanes()
        {
            return _context.Set<Plan>()
                           .Include(p => p.PlanServicios)
                               .ThenInclude(ps => ps.Servicio)
                           .Include(p => p.ServiciosAdicionales)
                               .ThenInclude(sa => sa.Servicio)
                           .Include(p => p.DispositivosPlanFamiliar)
                           .ToList();
        }

        // Método para obtener un plan por ID
        public PlanViewModel ObtenerPlanPorId(int planId)
        {
            var plan = _context.Planes
                .Include(p => p.PlanServicios)
                    .ThenInclude(ps => ps.Servicio)
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .FirstOrDefault(p => p.PlanId == planId);

            if (plan == null) return null;

            return new PlanViewModel
            {
                PlanId = plan.PlanId,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                Imagen = plan.Imagen,
                ServiciosIncluidos = plan.PlanServicios.Select(ps => ps.Servicio.Nombre).ToList(),
                ServiciosAdicionales = plan.ServiciosAdicionales.Select(sa => new ServicioAdicionalViewModel
                {
                    ServicioId = sa.ServicioId,
                    NombreServicio = sa.Servicio.Nombre,
                    PrecioOriginal = sa.Servicio.Precio,
                    Descuento = sa.Descuento,
                    PrecioConDescuento = AplicarDescuento(sa.Servicio.Precio, sa.Descuento)
                }).ToList()
            };
        }

        public decimal AplicarDescuento(decimal precioOriginal, decimal descuento)
        {
            return precioOriginal - (precioOriginal * descuento);
        }




        // Método para guardar una contratación de plan
        public async Task<int> GuardarContratacionAsync(int planId, string usuarioEmail)
        {
            var contratacion = new Contratacion
            {
                PlanId = planId,
                FechaContratacion = DateTime.Now
            };

            _context.Contratacion.Add(contratacion);
            await _context.SaveChangesAsync();

            return contratacion.ContratacionId;
        }

        // Método para guardar los servicios adicionales seleccionados en una contratación
        public async Task GuardarServiciosAdicionalesAsync(int contratacionId, List<int> serviciosAdicionalesSeleccionados)
        {
            var serviciosAdicionales = serviciosAdicionalesSeleccionados.Select(id => new ServicioAdicionalContratado
            {
                ContratacionId = contratacionId,
                ServicioAdicionalId = id
            });

            _context.ServicioAdicionalContratado.AddRange(serviciosAdicionales);
            await _context.SaveChangesAsync();
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

        public async Task<List<Servicio>> ObtenerServiciosIndividualesAsync()
        {
            var servicios = await _context.Servicios.ToListAsync();
            return servicios.Where(s => s.EsIndividual).ToList(); // Filtrado en memoria
        }



        // Método para recuperar solo el ID de un servicio adicional específico
        public int RecuperarServicioAdicionalId(int servicioId)
        {
            var servicioAdicional = _context.Set<ServicioAdicional>().Find(servicioId);
            return servicioAdicional != null ? servicioAdicional.ServicioId : 0;
        }
    }
}
