using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Excembly_vAlpha.Models;
using Excembly_vAlpha.Data;
using System;
using System.Linq;

namespace Excembly_vAlpha.Controllers
{
    public class PagoController : Controller
    {
        private readonly ExcemblyDbContext _context;

        public PagoController(ExcemblyDbContext context)
        {
            _context = context;
        }

        public IActionResult Crear(int contratacionId)
        {
            // Obtener el usuario autenticado (usando el claim)
            var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UsuarioId");
            if (usuarioIdClaim == null)
            {
                return Unauthorized("Usuario no autenticado.");
            }

            int usuarioId = int.Parse(usuarioIdClaim.Value);

            // Obtener la contratación
            var contratacion = _context.Contratacion
                .Include(c => c.Plan)
                .Include(c => c.ServiciosAdicionalesContratados)
                    .ThenInclude(sac => sac.ServicioAdicional)
                        .ThenInclude(sa => sa.Servicio) // Incluir el servicio relacionado para obtener el precio
                .FirstOrDefault(c => c.ContratacionId == contratacionId && c.UsuarioId == usuarioId);

            if (contratacion == null)
            {
                return NotFound("La contratación no fue encontrada.");
            }

            // Obtener la primera tarjeta guardada del usuario
            var tarjeta = _context.TarjetasGuardadas
                .FirstOrDefault(t => t.UsuarioId == usuarioId);

            if (tarjeta == null)
            {
                return BadRequest("No se encontraron tarjetas guardadas para el usuario.");
            }

            // Calcular el costo total
            decimal costoPlan = contratacion.Plan?.Precio ?? 0;
            decimal costoServiciosAdicionales = contratacion.ServiciosAdicionalesContratados?
                .Sum(sac => sac.ServicioAdicional.Servicio.Precio * (1 - sac.ServicioAdicional.Descuento)) ?? 0;

            decimal costoTotal = costoPlan + costoServiciosAdicionales;

            // Crear el modelo de pago
            var pago = new Pago
            {
                UsuarioId = usuarioId,
                ContratacionId = contratacion.ContratacionId,
                TarjetaId = tarjeta.TarjetaId,
                FechaPago = DateTime.Now,
                Monto = costoTotal,
                MetodoPago = tarjeta.Marca, // Ej. Visa, MasterCard
                Estado = "Completado",
                Referencia = Guid.NewGuid().ToString() // Generar referencia única
            };

            // Guardar el pago en la base de datos
            _context.Pagos.Add(pago);
            _context.SaveChanges();

            // Redirigir al índice de contrataciones
            return RedirectToAction("Index", "Contratacion");
        }
    }
}
