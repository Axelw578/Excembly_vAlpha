using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Controllers
{
    public class CuentaController : Controller
    {
        private readonly CuentaService _cuentaService;

        public CuentaController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        public IActionResult Index()
        {
            var usuarioId = ObtenerUsuarioIdDesdeClaims();
            if (usuarioId == null)
                return RedirectToAction("Login", "Auth");

            var cuenta = _cuentaService.ObtenerCuentaPorId(usuarioId.Value);
            if (cuenta == null)
                return NotFound("La cuenta no fue encontrada.");

            var viewModel = MapearCuentaAVista(cuenta);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(CuentaViewModel cuentaViewModel, IFormFile nuevaFotoPerfil)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", cuentaViewModel);
            }

            try
            {
                // Manejar nueva foto de perfil
                if (nuevaFotoPerfil != null && nuevaFotoPerfil.Length > 0)
                {
                    // Ruta para guardar las fotos de perfil en el servidor
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "perfil");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(nuevaFotoPerfil.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Crear el directorio si no existe
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Guardar la foto en el servidor
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await nuevaFotoPerfil.CopyToAsync(fileStream);
                    }

                    // Asignar la URL de la foto
                    cuentaViewModel.FotoPerfilUrl = "/imagenes/perfil/" + fileName;
                }

                // Mapear ViewModel a modelo
                var cuentaModelo = new Usuario
                {
                    UsuarioId = cuentaViewModel.UsuarioId,
                    Nombre = cuentaViewModel.Nombre,
                    Apellidos = cuentaViewModel.Apellidos,
                    Telefono = cuentaViewModel.Telefono,
                    FotoPerfilUrl = cuentaViewModel.FotoPerfilUrl,
                    Direccion = cuentaViewModel.Direccion != null ? new Direccion
                    {
                        Colonia = cuentaViewModel.Direccion.Colonia,
                        Calle = cuentaViewModel.Direccion.Calle,
                        NumeroEdificio = cuentaViewModel.Direccion.NumeroEdificio,
                        DescripcionEdificio = cuentaViewModel.Direccion.DescripcionEdificio,
                        ReferenciaEdificio = cuentaViewModel.Direccion.ReferenciaEdificio
                    } : null
                };

                // Validación del teléfono (ejemplo: asegurarse de que sea numérico y tenga 10 dígitos)
                if (!Regex.IsMatch(cuentaViewModel.Telefono, @"^\d{10}$"))
                {
                    ModelState.AddModelError("Telefono", "El teléfono debe tener 10 dígitos.");
                    return View("Index", cuentaViewModel);
                }

                // Actualizar cuenta
                _cuentaService.ActualizarCuenta(cuentaModelo);

                TempData["Mensaje"] = "Perfil actualizado correctamente.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar la cuenta: {ex.Message}");
                return View("Index", cuentaViewModel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ObtenerFotoPerfilUrl(int id)
        {
            var cuenta = _cuentaService.ObtenerCuentaPorId(id);
            if (cuenta?.FotoPerfilUrl == null)
                return NotFound("Foto de perfil no encontrada.");

            var fotoBytes = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", cuenta.FotoPerfilUrl.TrimStart('/')));
            return File(fotoBytes, "image/jpeg");
        }

        private int? ObtenerUsuarioIdDesdeClaims()
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null && int.TryParse(claim.Value, out int usuarioId) ? usuarioId : null;
        }

        private CuentaViewModel MapearCuentaAVista(Usuario cuenta)
        {
            return new CuentaViewModel
            {
                UsuarioId = cuenta.UsuarioId,
                Nombre = cuenta.Nombre,
                Apellidos = cuenta.Apellidos,
                CorreoElectronico = cuenta.CorreoElectronico,
                Telefono = cuenta.Telefono,
                FotoPerfilUrl = cuenta.FotoPerfilUrl,
                Direccion = cuenta.Direccion != null
                    ? new DireccionViewModel
                    {
                        Colonia = cuenta.Direccion.Colonia,
                        Calle = cuenta.Direccion.Calle,
                        NumeroEdificio = cuenta.Direccion.NumeroEdificio,
                        DescripcionEdificio = cuenta.Direccion.DescripcionEdificio,
                        ReferenciaEdificio = cuenta.Direccion.ReferenciaEdificio
                    }
                    : null
            };
        }
    }
}