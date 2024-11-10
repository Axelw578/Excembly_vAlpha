using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Excembly_vAlpha.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        // Acción para la vista principal de la empresa (Index)
        public async Task<IActionResult> Index()
        {
            // Obtener la información de la empresa a través del servicio
            var empresaData = await _empresaService.InformacionEmpresa();

            if (empresaData == null)
            {
                // Log de diagnóstico para saber si el objeto empresaData es null
                Console.WriteLine("No se encontró información de la empresa en la base de datos.");
                return NotFound("No se encontró información de la empresa.");
            }

            // Mapear los datos a EmpresaViewModel
            var empresaViewModel = new EmpresaViewModel
            {
                MisionVision = empresaData.MisionVision,
                HorarioTienda = empresaData.HorarioTienda,
                DomicilioTienda = empresaData.DomicilioTienda,
                Aclaracion = empresaData.Aclaracion,
                PoliticaCancelacion = empresaData.PoliticaCancelacion,
                ImagenMapa = empresaData.ImagenMapa
            };

            return View("Views/Empresa/Index.cshtml", empresaViewModel);
        }

        // Acción para la vista de reseñas (Reseñas)
        public async Task<IActionResult> Reseñas()
        {
            // Obtener los comentarios de los usuarios a través del servicio
            var comentarios = await _empresaService.ObtenerComentariosUsuarios();

            // Mapear los comentarios a una lista de ComentarioViewModel
            var reseñasViewModel = comentarios.Select(c => new ComentarioViewModel
            {
                NombreUsuario = c.Usuario.Nombre,
                Opinion = c.Opinion,
                FechaComentario = c.FechaComentario,
                FotoPerfilUrl = c.Usuario.FotoPerfilUrl,
                FotoComentarioUrl = c.FotoUrl // URL de la foto adjunta al comentario, si la hay
            }).ToList();

            // Crear un EmpresaViewModel que solo contiene la lista de reseñas
            var empresaViewModel = new EmpresaViewModel
            {
                Reseñas = reseñasViewModel
            };

            return View("Views/Empresa/Reseñas.cshtml", empresaViewModel);
        }
    }
}
