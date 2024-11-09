﻿using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public class LoginService
    {
        private readonly ExcemblyDbContext _context;

        public LoginService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Método para registrar un usuario
        public async Task<(bool Success, string Message)> RegistrarUsuarioAsync(Usuario usuario)
        {
            // Verificar si el correo ya está registrado
            if (await _context.Usuarios.AnyAsync(u => u.CorreoElectronico == usuario.CorreoElectronico))
            {
                return (false, "El correo ya está registrado.");
            }

            // Asignar rol predeterminado (Usuario)
            var rolUsuario = await _context.Roles.FirstOrDefaultAsync(r => r.TipoRol == "Usuario");
            if (rolUsuario == null)
            {
                return (false, "El rol de Usuario no existe en el sistema.");
            }

            usuario.RolId = rolUsuario.RolId;
            usuario.FechaRegistro = DateTime.Now;

            // Guardar usuario en la base de datos
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                // Registrar el error para diagnóstico (opcional)
                return (false, "Error al registrar el usuario.");
            }
        }

        // Método para iniciar sesión
        public async Task<(Usuario Usuario, string Message)> IniciarSesionAsync(string correo, string contraseña)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.CorreoElectronico == correo);

            if (usuario == null)
            {
                return (null, "El correo no está registrado.");
            }

            // Validar contraseña
            if (usuario.Contraseña != contraseña)
            {
                return (null, "Contraseña incorrecta.");
            }

            return (usuario, "Inicio de sesión exitoso.");
        }

        // Método para recuperar cuenta (Borrador)
        public async Task<string> RecuperarCuentaAsync(string correo)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
            if (usuario == null)
            {
                return "El correo no está registrado.";
            }

            // Aquí se implementaría la lógica de recuperación de cuenta
            return "Proceso de recuperación iniciado.";
        }
    }
}