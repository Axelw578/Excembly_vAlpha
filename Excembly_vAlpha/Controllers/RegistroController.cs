using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Mvc;
using Excembly_vAlpha.ViewModels;
using Newtonsoft.Json;
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
                    return RedirectToAction("Index", "Login");
                }

                // Si hubo un error en el registro, muestra el mensaje de error en la vista
                ModelState.AddModelError("", message);
            }
            catch (Exception ex)
            {
                // Manejo de excepción: registra el error en la consola y muestra un mensaje genérico en la vista
                Console.WriteLine("Error en el registro de usuario:");
                Console.WriteLine(JsonConvert.SerializeObject(new
                {
                    Error = ex.Message,
                    Detalles = ex.InnerException?.Message,
                    Fecha = DateTime.Now
                }, Formatting.Indented));

                ModelState.AddModelError("", "Ha ocurrido un error inesperado. Por favor, intente nuevamente.");
            }

            // Retorna la vista de registro con los errores de validación
            return View("~/Views/Registro/Registrar.cshtml", model);
        }
    }
}
