using System;
using System.Linq;
using System.Threading.Tasks;
using Excembly_vAlpha.Data;
using Excembly_vAlpha.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Services
{
    public class TarjetaService
    {
        private readonly ExcemblyDbContext _context;
        private readonly ILogger<TarjetaService> _logger;

        public TarjetaService(ExcemblyDbContext context, ILogger<TarjetaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TarjetaGuardada>> ObtenerTarjetasGuardadasAsync(int usuarioId)
        {
            try
            {
                var tarjetas = await _context.TarjetasGuardadas
                    .Where(t => t.UsuarioId == usuarioId)
                    .ToListAsync();

                if (!tarjetas.Any())
                {
                    _logger.LogWarning("No se encontraron tarjetas guardadas para el usuario con ID {UsuarioId}.", usuarioId);
                }
                return tarjetas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las tarjetas guardadas para el usuario con ID {UsuarioId}", usuarioId);
                return new List<TarjetaGuardada>();
            }
        }

        // Agrega este método si aún no existe
        public async Task<(bool IsSuccess, string ErrorMessage)> ActualizarTarjetaAsync(TarjetaGuardada tarjeta)
        {
            try
            {

                var tarjetaExistente = await _context.TarjetasGuardadas.FindAsync(tarjeta.TarjetaId);
                if (tarjetaExistente == null)
                {
                    return (false, "Tarjeta no encontrada");
                }

                tarjetaExistente.NombreTitular = tarjeta.NombreTitular;
                tarjetaExistente.NumeroTarjeta = tarjeta.NumeroTarjeta;
                tarjetaExistente.FechaExpiracion = tarjeta.FechaExpiracion;
                tarjetaExistente.CVV = tarjeta.CVV;
                tarjetaExistente.Banco = tarjeta.Banco;
                tarjetaExistente.Marca = tarjeta.Marca;
                tarjetaExistente.TipoTarjeta = tarjeta.TipoTarjeta;

                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<string> ObtenerTarjetaCensuradaAsync(int usuarioId)
        {
            try
            {
                var tarjeta = await _context.TarjetasGuardadas
                    .FirstOrDefaultAsync(t => t.UsuarioId == usuarioId);

                return tarjeta != null ? $"**** **** **** {tarjeta.NumeroTarjeta[^4..]}" : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tarjeta censurada para el usuario {UsuarioId}", usuarioId);
                return null;
            }
        }

        public async Task<TarjetaGuardada> ObtenerTarjetaPorUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var tarjeta = await _context.TarjetasGuardadas
                    .FirstOrDefaultAsync(t => t.UsuarioId == usuarioId);

                if (tarjeta == null)
                {
                    _logger.LogWarning("No se encontró una tarjeta guardada para el usuario con ID {UsuarioId}.", usuarioId);
                }
                return tarjeta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la tarjeta para el usuario con ID {UsuarioId}", usuarioId);
                return null;
            }
        }

        public async Task<Result> AgregarTarjetaAsync(TarjetaGuardada tarjeta)
        {
            try
            {
                _context.TarjetasGuardadas.Add(tarjeta);
                int resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return new Result { IsSuccess = true };
                }

                return new Result { IsSuccess = false, ErrorMessage = "No se pudo guardar la tarjeta." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la tarjeta.");
                return new Result { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        public class Result
        {
            public bool IsSuccess { get; set; }
            public string ErrorMessage { get; set; }
        }



        public async Task<bool> EliminarTarjetaAsync(int tarjetaId)
        {
            try
            {
                var tarjeta = await _context.TarjetasGuardadas.FindAsync(tarjetaId);
                if (tarjeta != null)
                {
                    _context.TarjetasGuardadas.Remove(tarjeta);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tarjeta con ID {TarjetaId}", tarjetaId);
                return false;
            }
        }

        public async Task<Result> EditarTarjetaAsync(int tarjetaId, TarjetaGuardada tarjetaActualizada)
        {
            try
            {
                var tarjeta = await _context.TarjetasGuardadas.FindAsync(tarjetaId);

                if (tarjeta == null)
                {
                    _logger.LogWarning("No se encontró la tarjeta con ID {TarjetaId} para editar.", tarjetaId);
                    return new Result { IsSuccess = false, ErrorMessage = "Tarjeta no encontrada." };
                }

                // Actualizar los campos necesarios
                tarjeta.NombreTitular = tarjetaActualizada.NombreTitular;
                tarjeta.NumeroTarjeta = tarjetaActualizada.NumeroTarjeta;
                tarjeta.FechaExpiracion = tarjetaActualizada.FechaExpiracion;
                tarjeta.CVV = tarjetaActualizada.CVV;
                tarjeta.Banco = tarjetaActualizada.Banco;

                _context.TarjetasGuardadas.Update(tarjeta);
                int resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return new Result { IsSuccess = true };
                }

                return new Result { IsSuccess = false, ErrorMessage = "No se pudieron guardar los cambios en la tarjeta." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la tarjeta con ID {TarjetaId}", tarjetaId);
                return new Result { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }


        public async Task<TarjetaGuardada> ObtenerTarjetaPorIdAsync(int tarjetaId)
        {
            return await _context.TarjetasGuardadas.FirstOrDefaultAsync(t => t.TarjetaId == tarjetaId);
        }


        public async Task<TarjetaGuardada> SeleccionarTarjetaAsync(int tarjetaId)
        {
            try
            {
                var tarjeta = await _context.TarjetasGuardadas.FirstOrDefaultAsync(t => t.TarjetaId == tarjetaId);
                if (tarjeta == null)
                {
                    _logger.LogWarning("No se encontró la tarjeta con ID {TarjetaId}.", tarjetaId);
                }
                return tarjeta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al seleccionar tarjeta con ID {TarjetaId}", tarjetaId);
                return null;
            }
        }
    }
}
