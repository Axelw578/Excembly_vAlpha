using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            return View("~/Views/Login/Index.cshtml"); // Apunta a Index.cshtml
        }

        // Procesa el inicio de sesión del usuario
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Login/Index.cshtml", model); // Retorna a la misma vista con los errores
            }

            var resultado = await _loginService.IniciarSesionAsync(model.Correo, model.Contraseña);

            if (resultado.Usuario != null)
            {
                var usuario = resultado.Usuario;

                // Crear claims del usuario, incluyendo UsuarioId y la URL de la imagen de perfil
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Surname, usuario.Apellidos), // Cambiado a ClaimTypes.Surname
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
            return View("~/Views/Login/Index.cshtml", model); // Retorna a la vista con el modelo en caso de error
        }

        // Cierra sesión del usuario
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login"); // Redirige a la vista de inicio de sesión
        }
    }
}
