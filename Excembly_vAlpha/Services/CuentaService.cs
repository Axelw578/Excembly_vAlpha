using System;
using System.Linq;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;  // Asegúrate de agregar esta directiva
using Newtonsoft.Json;

namespace Excembly_vAlpha.Services
{
    public class CuentaService
    {
        private readonly ExcemblyDbContext _context;
        private readonly ILogger<CuentaService> _logger; // Añadimos el logger

        public CuentaService(ExcemblyDbContext context, ILogger<CuentaService> logger)  // Inyección del logger
        {
            _context = context;
            _logger = logger;  // Asignamos el logger
        }

        // Obtener cuenta por usuarioId
        public Usuario ObtenerCuentaPorId(int usuarioId)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
                if (usuario == null)
                {
                    _logger.LogWarning("Usuario con ID {UsuarioId} no encontrado.", usuarioId);  // Registro de advertencia si no se encuentra el usuario
                }
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cuenta con ID {UsuarioId}.", usuarioId); // Registro del error
                return null;
            }
        }

        // Actualizar cuenta
        public void ActualizarCuenta(Usuario usuario)
        {
            try
            {
                var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);
                if (usuarioExistente != null)
                {
                    // Actualizar los campos de usuario
                    usuarioExistente.Nombre = usuario.Nombre;
                    usuarioExistente.Apellidos = usuario.Apellidos;
                    usuarioExistente.Telefono = usuario.Telefono;
                    usuarioExistente.FotoPerfilUrl = usuario.FotoPerfilUrl;

                    _context.SaveChanges();
                    _logger.LogInformation("Cuenta del usuario con ID {UsuarioId} actualizada correctamente.", usuario.UsuarioId); // Registro de éxito
                }
                else
                {
                    _logger.LogWarning("Usuario con ID {UsuarioId} no encontrado para actualizar.", usuario.UsuarioId); // Registro de advertencia si no se encuentra el usuario
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la cuenta del usuario con ID {UsuarioId}.", usuario.UsuarioId);  // Registro del error
            }
        }
    }
}
