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

        // Inyectar el servicio EmpresaService
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
                // Si no se encuentra la información, redirigir o mostrar un mensaje adecuado
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

            // Pasar el modelo a la vista
            return View(empresaViewModel);
        }

        // Acción para la vista de reseñas (Reseñas)
        public async Task<IActionResult> Reseñas()
        {
            // Obtener los comentarios de los usuarios a través del servicio
            var comentarios = await _empresaService.ObtenerComentariosUsuarios();

            if (comentarios == null || !comentarios.Any())
            {
                // Si no se encuentran comentarios, mostrar un mensaje adecuado
                ViewBag.Mensaje = "No hay comentarios disponibles.";
            }

            // Mapear los comentarios a una lista de ComentarioViewModel
            var reseñasViewModel = comentarios.Select(c => new ComentarioViewModel
            {
                NombreUsuario = c.NombreUsuario,       // Nombre del usuario
                Opinion = c.Opinion,                   // Opinión del comentario
                FechaComentario = c.FechaComentario,  // Fecha del comentario
                FotoPerfilUrl = c.FotoPerfilUrl,       // Foto de perfil del usuario
                FotoComentarioUrl = c.FotoComentarioUrl // Foto adjunta al comentario
            }).ToList();

            // Crear un EmpresaViewModel que solo contiene la lista de reseñas
            var empresaViewModel = new EmpresaViewModel
            {
                // Asignar la lista de comentarios a Reseñas
                Reseñas = reseñasViewModel
            };

            // Pasar el modelo a la vista
            return View(empresaViewModel);
        }
        public IActionResult Privacidad()
        {
            return View();
        }
        public IActionResult Terminos()
        {
            return View();
        }

    }
}