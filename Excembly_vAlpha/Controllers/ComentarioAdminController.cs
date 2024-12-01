using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Excembly_vAlpha.Controllers
{
    public class ComentariosAdminController : Controller
    {
        private readonly IComentarioAdminService _comentarioAdminService;

        public ComentariosAdminController(IComentarioAdminService comentarioAdminService)
        {
            _comentarioAdminService = comentarioAdminService;
        }

        // Acción para mostrar todos los comentarios
        public async Task<IActionResult> Index()
        {
            var comentarios = await _comentarioAdminService.ObtenerTodosComentariosAsync();
            return View(comentarios);
        }

        // Acción para obtener comentarios filtrados
        public async Task<IActionResult> Filtrar(string ordenPor = "FechaComentario", bool ascendente = true)
        {
            var comentarios = await _comentarioAdminService.ObtenerComentariosFiltradosAsync(ordenPor, ascendente);
            return View("Index", comentarios); // Reutiliza la vista Index
        }

        // Acción para ver el detalle de un comentario
        public async Task<IActionResult> Detalle(int id)
        {
            try
            {
                var comentarioDetalle = await _comentarioAdminService.ObtenerDetalleComentarioAsync(id);
                return View(comentarioDetalle);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Acción para obtener un comentario por su ID (puede ser útil para editar o eliminar)
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var comentario = await _comentarioAdminService.ObtenerComentarioPorIdAsync(id);
                return View("Detalle", comentario); // Redirige a la vista Detalle para mostrarlo
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
