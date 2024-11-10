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

                // Crea claims del usuario, incluyendo UsuarioId y la URL de la imagen de perfil
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Surname, usuario.Apellidos),
                    new Claim(ClaimTypes.Email, usuario.CorreoElectronico),
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),  // Claim para el UsuarioId
                    new Claim("URLFotoPerfil", usuario.FotoPerfilUrl ?? "/img/default-user.png")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Mantener la sesión activa
                };

                // Inicia la sesión con las claims del usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home"); // Redirige al controlador Home
            }

            // Si hay error, lo agrega al estado del modelo y vuelve a mostrar la vista de inicio de sesión
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
