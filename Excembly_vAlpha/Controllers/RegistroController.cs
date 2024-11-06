using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Mvc;
using Excembly_vAlpha.ViewModels;
using System;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class RegistroController : Controller
    {
        private readonly LoginService _loginService;

        public RegistroController(LoginService loginService)
        {
            _loginService = loginService;
        }

        // Muestra la vista de registro
        [HttpGet]
        public IActionResult Registrar()
        {
            // Retorna la vista Registrar ubicada en Views/Registro
            return View("~/Views/Registro/Registrar.cshtml", new UsuarioRegistroViewModel());
        }

        // Procesa el registro del usuario
        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegistroViewModel model)
        {
            // Verifica si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Retorna la vista con el modelo en caso de errores de validación
                return View("~/Views/Registro/Registrar.cshtml", model);
            }

            // Asigna la URL de la imagen de perfil o una imagen por defecto
            var urlFotoPerfil = string.IsNullOrEmpty(model.FotoPerfilUrl) ? "/img/default-user.png" : model.FotoPerfilUrl;

            // Crea un nuevo usuario a partir del modelo
            var nuevoUsuario = new Usuario
            {
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                CorreoElectronico = model.Correo,
                Contraseña = model.Contraseña,
                FotoPerfilUrl = urlFotoPerfil,
                Telefono = model.Telefono
            };

            try
            {
                // Intenta registrar el nuevo usuario
                var (success, message) = await _loginService.RegistrarUsuarioAsync(nuevoUsuario);

                // Verifica el resultado del registro
                if (success)
                {
                    TempData["RegistroExitoso"] = "Usuario registrado con éxito.";
                    // Redirige a la acción de Login en el controlador LoginController
                    return RedirectToAction("Login", "Index");
                }

                ModelState.AddModelError("", message);
            }
            catch (Exception ex)
            {
                // Manejo de excepción: 
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
            }

            // Retorna error
            return View("~/Views/Registro/Registrar.cshtml", model);
        }
    }
}
