using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Excembly_vAlpha.Services
{
    public class DireccionService
    {
        private readonly ExcemblyDbContext _context;

        public DireccionService(ExcemblyDbContext context)
        {
            _context = context;
        }

        // Obtener la dirección de un usuario
        public async Task<DireccionViewModel> ObtenerDireccionAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Direccion)
                .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);

            if (usuario == null)
                return null;

            // Crear el ViewModel para la dirección
            var viewModel = new DireccionViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                CorreoElectronico = usuario.CorreoElectronico,
                Telefono = usuario.Telefono,
                FotoPerfilUrl = usuario.FotoPerfilUrl,
                TieneDireccion = usuario.Direccion != null,
                DireccionId = usuario.Direccion?.DireccionId,
                Colonia = usuario.Direccion?.Colonia,
                Calle = usuario.Direccion?.Calle,
                NumeroEdificio = usuario.Direccion?.NumeroEdificio,
                DescripcionEdificio = usuario.Direccion?.DescripcionEdificio,
                ReferenciaEdificio = usuario.Direccion?.ReferenciaEdificio
            };

            return viewModel;
        }

        // Guardar o actualizar la dirección del usuario
        public async Task<bool> GuardarDireccionAsync(DireccionViewModel model)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Direccion)
                .FirstOrDefaultAsync(u => u.UsuarioId == model.UsuarioId);

            if (usuario == null)
                return false;

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
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
