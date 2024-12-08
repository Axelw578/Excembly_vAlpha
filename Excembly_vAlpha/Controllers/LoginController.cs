using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        // Muestra la vista de inicio de sesión
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        // Procesa el inicio de sesión del usuario
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Login/Index.cshtml", model);
            }

            var resultado = await _loginService.IniciarSesionAsync(model.Correo, model.Contraseña);

            if (resultado.Usuario != null)
            {
                var usuario = resultado.Usuario;

                // Crear los claims del usuario
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Surname, usuario.Apellidos),
            new Claim(ClaimTypes.Email, usuario.CorreoElectronico),
            new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()), // UsuarioId
            new Claim("URLFotoPerfil", usuario.FotoPerfilUrl ?? "/img/default-user.png"),
            new Claim(ClaimTypes.Role, usuario.Rol.TipoRol) // Rol del usuario
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Mantener la sesión activa
                };

                // Inicia sesión con las claims del usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Redirigir según el rol del usuario
                switch (usuario.RolId)
                {
                    case 1: // Usuario
                        TempData["Bienvenida"] = $"¡Bienvenido, {usuario.Nombre}!";
                        return RedirectToAction("Index", "Empresa", new { area = "Usuario" });

                    case 2: // Técnico
                        TempData["Bienvenida"] = $"¡Bienvenido Técnico, {usuario.Nombre}!";
                        return RedirectToAction("Index", "ContratacionTecnico", new { area = "Tecnico" });

                    case 3: // Administrador
                        TempData["Bienvenida"] = $"¡Bienvenido Administrador, {usuario.Nombre}!";
                        return RedirectToAction("Index", "ContratacionAdmin", new { area = "Admin" });

                    default: // Rol no reconocido
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        TempData["Error"] = "Rol no válido.";
                        return RedirectToAction("Index");
                }
            }

            // Si hay error, mostrar el mensaje
            ModelState.AddModelError("", resultado.Message);
            return View("~/Views/Login/Index.cshtml", model);
        }

        // Cierra sesión del usuario
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Finaliza la sesión del usuario y elimina las cookies de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Redirige al menu
        }
    }
}
