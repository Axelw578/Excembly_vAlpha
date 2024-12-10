using Excembly_vAlpha.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Excembly_vAlpha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            // Obtener el rol del usuario autenticado
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            // Redirigir seg n el rol
            switch (rol)
            {
                case "Usuario":
                    return View("~/Views/Empresa/Index.cshtml");

                case "T cnico":
                    return View("~/Views/ContratacionTecnico/Index.cshtml");

                case "Administrador":
                    return View("~/Views/ContratacionAdmin/Index.cshtml");

                default:
                    return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}