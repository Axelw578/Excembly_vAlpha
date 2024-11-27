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
    public class ServicioAdicionalService
    {
        private readonly ExcemblyDbContext _context;
        private readonly IMapper _mapper; // Inyectar AutoMapper

        public ServicioAdicionalService(ExcemblyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener los servicios adicionales por el ID del Plan
        public async Task<List<ServicioAdicionalViewModel>> ObtenerServiciosAdicionalesPorPlanIdAsync(int planId)
        {
            try
            {
                var serviciosAdicionales = await _context.ServicioAdicional
                    .Where(sa => sa.PlanId == planId)
                    .Include(sa => sa.Servicio)
                    .ToListAsync();

                return _mapper.Map<List<ServicioAdicionalViewModel>>(serviciosAdicionales); // Usar AutoMapper
            }
            catch (Exception ex)
            {
                // Imprimir error en la consola usando Newtonsoft.Json
                var errorJson = JsonConvert.SerializeObject(new { error = ex.Message });
                Console.WriteLine(errorJson);
                return new List<ServicioAdicionalViewModel>(); // Retornar una lista vacía en caso de error
            }
        }

        // Obtener todos los servicios adicionales
        public async Task<List<ServicioAdicionalViewModel>> ObtenerServiciosAdicionalesAsync()
        {
            try
            {
                var serviciosAdicionales = await _context.ServiciosAdicionales.ToListAsync();
                return _mapper.Map<List<ServicioAdicionalViewModel>>(serviciosAdicionales); // Usar AutoMapper
            }
            catch (Exception ex)
            {
                // Imprimir error en la consola usando Newtonsoft.Json
                var errorJson = JsonConvert.SerializeObject(new { error = ex.Message });
                Console.WriteLine(errorJson);
                return new List<ServicioAdicionalViewModel>(); // Retornar una lista vacía en caso de error
            }
        }

        // Obtener los servicios adicionales por sus IDs
        public async Task<List<ServicioAdicionalViewModel>> ObtenerServiciosAdicionalesPorIdsAsync(List<int> servicioAdicionalIds)
        {
            try
            {
                var serviciosAdicionales = await _context.ServicioAdicional
                    .Where(sa => servicioAdicionalIds.Contains(sa.Id))
                    .Include(sa => sa.Servicio)
                    .ToListAsync();

                return _mapper.Map<List<ServicioAdicionalViewModel>>(serviciosAdicionales); // Usar AutoMapper
            }
            catch (Exception ex)
            {
                // Imprimir error en la consola usando Newtonsoft.Json
                var errorJson = JsonConvert.SerializeObject(new { error = ex.Message });
                Console.WriteLine(errorJson);
                return new List<ServicioAdicionalViewModel>(); // Retornar una lista vacía en caso de error
            }
        }
    }
}
