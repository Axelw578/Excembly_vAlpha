using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper; // Incluir AutoMapper
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json; // Incluir Newtonsoft.Json para manejar errores

namespace Excembly_vAlpha.Services
{
    public class PlanesService
    {
        private readonly ExcemblyDbContext _context;
        private readonly IMapper _mapper; // Inyectar AutoMapper

        public PlanesService(ExcemblyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los planes
        public async Task<IEnumerable<PlanViewModel>> ObtenerTodosLosPlanesAsync()
        {
            var planes = await _context.Planes
                .Include(p => p.PlanServicios)
                    .ThenInclude(ps => ps.Servicio)
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlanViewModel>>(planes); // Usar AutoMapper para mapear a ViewModel
        }

        // Obtener todos los planes familiares
        public async Task<IEnumerable<PlanViewModel>> ObtenerTodosLosPlanesFamiliarAsync()
        {
            var planes = await _context.Set<Plan>()
                .Include(p => p.PlanServicios)
                    .ThenInclude(ps => ps.Servicio)
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .Include(p => p.DispositivosPlanFamiliar)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PlanViewModel>>(planes); // Usar AutoMapper para mapear a ViewModel
        }

        // Obtener todos los planes (sin asincronía)
        public IEnumerable<PlanViewModel> ObtenerTodosLosPlanes()
        {
            var planes = _context.Set<Plan>()
                .Include(p => p.PlanServicios)
                    .ThenInclude(ps => ps.Servicio)
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .Include(p => p.DispositivosPlanFamiliar)
                .ToList();

            return _mapper.Map<IEnumerable<PlanViewModel>>(planes); // Usar AutoMapper para mapear a ViewModel
        }

        // Obtener plan por ID
        public PlanViewModel ObtenerPlanPorId(int planId)
        {
            var plan = _context.Planes
                .Include(p => p.PlanServicios)
                    .ThenInclude(ps => ps.Servicio)
                .Include(p => p.ServiciosAdicionales)
                    .ThenInclude(sa => sa.Servicio)
                .FirstOrDefault(p => p.PlanId == planId);

            if (plan == null) return null;

            // Usar AutoMapper para mapear a ViewModel
            return _mapper.Map<PlanViewModel>(plan);
        }

        // Método para aplicar descuento
        public decimal AplicarDescuento(decimal precioOriginal, decimal descuento)
        {
            return precioOriginal - (precioOriginal * descuento);
        }

        // Método para guardar una contratación de plan
        public async Task<int> GuardarContratacionAsync(int planId, string usuarioEmail)
        {
            try
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
            catch (Exception ex)
            {
                // Imprimir error en la consola usando Newtonsoft.Json
                var errorJson = JsonConvert.SerializeObject(new { error = ex.Message });
                Console.WriteLine(errorJson);
                return 0; // Retornar un valor por defecto en caso de error
            }
        }

        // Método para guardar los servicios adicionales seleccionados en una contratación
        public async Task GuardarServiciosAdicionalesAsync(int contratacionId, List<int> serviciosAdicionalesSeleccionados)
        {
            try
            {
                var serviciosAdicionales = serviciosAdicionalesSeleccionados.Select(id => new ServicioAdicionalContratado
                {
                    ContratacionId = contratacionId,
                    ServicioAdicionalId = id
                });

                _context.ServicioAdicionalContratado.AddRange(serviciosAdicionales);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Imprimir error en la consola usando Newtonsoft.Json
                var errorJson = JsonConvert.SerializeObject(new { error = ex.Message });
                Console.WriteLine(errorJson);
            }
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

        // Método para obtener los servicios individuales
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
