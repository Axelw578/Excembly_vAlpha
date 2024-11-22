using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public class DireccionService
    {
        private readonly ExcemblyDbContext _context;
        private readonly ILogger<DireccionService> _logger;

        public DireccionService(ExcemblyDbContext context, ILogger<DireccionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Método para editar una dirección existente
        public async Task<bool> EditarDireccionAsync(DireccionViewModel model)
        {
            try
            {
                _logger.LogInformation("Iniciando la edición de la dirección. Datos de entrada: {Model}", SerializeToJson(model));

                var usuario = await _context.Usuarios
                    .Include(u => u.Direccion)
                    .FirstOrDefaultAsync(u => u.UsuarioId == model.UsuarioId);

                if (usuario == null || usuario.DireccionId != model.DireccionId)
                {
                    _logger.LogWarning("Usuario o dirección no válidos. Datos: {Model}", SerializeToJson(model));
                    return false;
                }

                var direccionExistente = await _context.Direcciones
                    .FirstOrDefaultAsync(d => d.DireccionId == model.DireccionId);

                if (direccionExistente == null)
                {
                    _logger.LogWarning("La dirección con ID {DireccionId} no existe.", model.DireccionId);
                    return false;
                }

                direccionExistente.Colonia = model.Colonia;
                direccionExistente.Calle = model.Calle;
                direccionExistente.NumeroEdificio = model.NumeroEdificio;
                direccionExistente.DescripcionEdificio = model.DescripcionEdificio;
                direccionExistente.ReferenciaEdificio = model.ReferenciaEdificio;

                _context.Direcciones.Update(direccionExistente);

                await _context.SaveChangesAsync();
                _logger.LogInformation("Dirección actualizada con éxito: {Direccion}", SerializeToJson(direccionExistente));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la dirección. Datos: {Model}", SerializeToJson(model));
                return false;
            }
        }

        // Obtener la dirección de un usuario
        public async Task<DireccionViewModel> ObtenerDireccionAsync(int usuarioId)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.Direccion)
                    .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);

                if (usuario == null)
                {
                    _logger.LogWarning("Usuario con ID {UsuarioId} no encontrado.", usuarioId);
                    return null;
                }

                var viewModel = new DireccionViewModel
                {
                    UsuarioId = usuario.UsuarioId,
                    TieneDireccion = usuario.Direccion != null,
                    DireccionId = usuario.Direccion?.DireccionId,
                    Colonia = usuario.Direccion?.Colonia,
                    Calle = usuario.Direccion?.Calle,
                    NumeroEdificio = usuario.Direccion?.NumeroEdificio,
                    DescripcionEdificio = usuario.Direccion?.DescripcionEdificio,
                    ReferenciaEdificio = usuario.Direccion?.ReferenciaEdificio
                };

                _logger.LogInformation("Dirección obtenida correctamente: {Direccion}", SerializeToJson(viewModel));
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la dirección del usuario con ID {UsuarioId}.", usuarioId);
                return null;
            }
        }

        // Guardar o actualizar la dirección del usuario
        public async Task<bool> GuardarDireccionAsync(DireccionViewModel model)
        {
            try
            {
                _logger.LogInformation("Iniciando el guardado de la dirección. Datos de entrada: {Model}", SerializeToJson(model));

                var usuario = await _context.Usuarios
                    .Include(u => u.Direccion)
                    .FirstOrDefaultAsync(u => u.UsuarioId == model.UsuarioId);

                if (usuario == null)
                {
                    _logger.LogWarning("Usuario con ID {UsuarioId} no encontrado.", model.UsuarioId);
                    return false;
                }

                if (model.DireccionId == null)
                {
                    var nuevaDireccion = new Direccion
                    {

                        Colonia = model.Colonia,
                        Calle = model.Calle,
                        NumeroEdificio = model.NumeroEdificio,
                        DescripcionEdificio = model.DescripcionEdificio,
                        ReferenciaEdificio = model.ReferenciaEdificio
                    };

                    _context.Direcciones.Add(nuevaDireccion);
                    await _context.SaveChangesAsync();

                    usuario.DireccionId = nuevaDireccion.DireccionId;
                    usuario.Direccion = nuevaDireccion;
                    _logger.LogInformation("Nueva dirección creada: {Direccion}", SerializeToJson(nuevaDireccion));
                }
                else
                {
                    var direccionExistente = await _context.Direcciones
                        .FirstOrDefaultAsync(d => d.DireccionId == model.DireccionId);

                    if (direccionExistente != null)
                    {
                        direccionExistente.Colonia = model.Colonia;
                        direccionExistente.Calle = model.Calle;
                        direccionExistente.NumeroEdificio = model.NumeroEdificio;
                        direccionExistente.DescripcionEdificio = model.DescripcionEdificio;
                        direccionExistente.ReferenciaEdificio = model.ReferenciaEdificio;

                        _context.Direcciones.Update(direccionExistente);
                        _logger.LogInformation("Dirección existente actualizada: {Direccion}", SerializeToJson(direccionExistente));
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la dirección. Datos: {Model}", SerializeToJson(model));
                return false;
            }
        }

        // Método auxiliar para serializar objetos a JSON con Newtonsoft.Json
        private string SerializeToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al serializar el objeto a JSON.");
                return "{}";
            }
        }
    }
}
