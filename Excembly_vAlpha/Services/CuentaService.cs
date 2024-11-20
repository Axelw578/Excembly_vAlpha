using System;
using System.Linq;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class CuentaService
    {
        private readonly ExcemblyDbContext _context;

        public CuentaService(ExcemblyDbContext context)
        {
            _context = context;
        }

        public Usuario ObtenerCuentaPorId(int id)
        {
            return _context.Usuarios
                           .Include(u => u.Direccion)
                           .FirstOrDefault(u => u.UsuarioId == id);
        }

        public void ActualizarCuenta(Usuario cuenta)
        {
            var cuentaExistente = _context.Usuarios
                                          .Include(u => u.Direccion)
                                          .FirstOrDefault(u => u.UsuarioId == cuenta.UsuarioId);

            if (cuentaExistente == null)
                throw new Exception("Cuenta no encontrada.");

            // Actualizar propiedades principales
            cuentaExistente.Nombre = cuenta.Nombre;
            cuentaExistente.Apellidos = cuenta.Apellidos;
            cuentaExistente.Telefono = cuenta.Telefono;
            cuentaExistente.FotoPerfilUrl = cuenta.FotoPerfilUrl;

            // Manejar dirección
            if (cuenta.Direccion != null)
            {
                if (cuentaExistente.Direccion == null)
                {
                    cuentaExistente.Direccion = new Direccion();
                    _context.Direcciones.Add(cuentaExistente.Direccion); // Agregar nueva dirección
                }

                cuentaExistente.Direccion.Colonia = cuenta.Direccion.Colonia;
                cuentaExistente.Direccion.Calle = cuenta.Direccion.Calle;
                cuentaExistente.Direccion.NumeroEdificio = cuenta.Direccion.NumeroEdificio;
                cuentaExistente.Direccion.DescripcionEdificio = cuenta.Direccion.DescripcionEdificio;
                cuentaExistente.Direccion.ReferenciaEdificio = cuenta.Direccion.ReferenciaEdificio;
            }

            // Guardar cambios
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error al guardar cambios: {ex.Message}", ex);
            }
        }
    }
}