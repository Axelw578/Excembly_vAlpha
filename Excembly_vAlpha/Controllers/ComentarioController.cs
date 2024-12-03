using System.Security.Claims;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Excembly_vAlpha.Controllers
{
    public class ComentarioController : Controller
    {
        private readonly ExcemblyDbContext _context;

        public ComentarioController(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Mostrar formulario de agregar comentario
        public IActionResult Agregar(int contratacionId)
        {
            var viewModel = new AgregarComentarioViewModel
            {
                ContratacionId = contratacionId
            };
            return View(viewModel);
        }

        // Procesar la creación de un comentario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(AgregarComentarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Obtener el ID del usuario autenticado desde los claims
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Login", "Account");
            }

            var nuevoComentario = new Comentario
            {
                ContratacionId = model.ContratacionId,
                UsuarioId = int.Parse(usuarioId),
                Opinion = model.Opinion,
                FotoUrl = model.FotoUrl,
                FechaComentario = DateTime.Now
            };

            _context.Comentario.Add(nuevoComentario);
            _context.SaveChanges();

            return RedirectToAction("Detalles", "Contratacion", new { contratacionId = model.ContratacionId });
        }
    }
}
