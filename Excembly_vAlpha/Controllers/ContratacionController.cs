﻿using Excembly_vAlpha.Models;
using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace Excembly_vAlpha.Controllers
{
    public class ContratacionController : Controller
    {
        private readonly ContratacionService _contratacionService;
        private readonly PlanesService _planesService;
        private readonly ServicioAdicionalService _servicioAdicionalService;
        private readonly ServiciosService _serviciosService;

        public ContratacionController(
            ContratacionService contratacionService,
            PlanesService planesService,
            ServicioAdicionalService servicioAdicionalService,
            ServiciosService serviciosService)
        {
            _contratacionService = contratacionService;
            _planesService = planesService;
            _servicioAdicionalService = servicioAdicionalService;
            _serviciosService = serviciosService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Index", "Login");
            }

            var resultado = await _contratacionService.ObtenerContratacionesPorUsuarioAsync(int.Parse(usuarioId));
            if (!resultado.Success)
            {
                ViewBag.Error = resultado.Message;
                return View(new List<ContratacionViewModel>());
            }

            // Mapeo de las contrataciones
            var contrataciones = resultado.Contrataciones.Select(c => new ContratacionViewModel
            {
                ContratacionId = c.ContratacionId,
                PlanId = c.Plan?.PlanId ?? 0,
                NombrePlan = c.Plan?.Nombre,
                PrecioPlan = c.Plan?.Precio ?? 0,
                Estado = c.Estado,
                FechaContratacion = c.FechaContratacion,
                ServiciosAdicionalesContratados = c.ServiciosAdicionalesContratados?.Select(sa => new ServicioAdicionalViewModel
                {
                    NombreServicio = sa.ServicioAdicional.Servicio.Nombre,
                    PrecioOriginal = sa.ServicioAdicional.Servicio.Precio,
                    PrecioConDescuento = sa.ServicioAdicional.Servicio.Precio - (sa.ServicioAdicional.Servicio.Precio * sa.ServicioAdicional.Descuento),
                    Descuento = sa.ServicioAdicional.Descuento
                }).ToList(),
                NombreTecnico = c.Cita?.Tecnico?.Nombre,
                FotoTecnicoUrl = c.Cita?.Tecnico?.Foto,
                DireccionDomicilio = c.Cita?.Direccion?.Colonia
            }).ToList();

            if (contrataciones == null || !contrataciones.Any())
            {
                ViewBag.SinContrataciones = "SIN CONTRATACIONES";
                return View(new List<ContratacionViewModel>());
            }

            return View(contrataciones);
        }

        // Acción para crear una nueva contratación
        public async Task<IActionResult> Crear(int? planId, int? servicioId)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Index", "Login");
            }

            // Obtenemos los planes y servicios
            var planes = await _planesService.ObtenerTodosLosPlanesAsync();
            var serviciosIndividuales = await _serviciosService.ObtenerServiciosIndividualesAsync();

            var planViewModels = planes.Select(plan => new PlanViewModel
            {
                PlanId = plan.PlanId,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                Imagen = plan.Imagen,
                ServiciosIncluidos = plan.PlanServicios.Select(ps => ps.Servicio.Nombre).ToList(),
                ServiciosAdicionales = new List<ServicioAdicionalViewModel>()
            }).ToList();

            var servicioViewModels = serviciosIndividuales.Select(s => new ServicioViewModel
            {
                ServicioId = s.ServicioId,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                Precio = s.Precio,
                ImagenUrl = s.Imagen
            }).ToList();

            var viewModel = new ContratacionViewModel
            {
                Planes = planViewModels,
                ServiciosIndividualesDisponibles = servicioViewModels,
                UsuarioId = int.Parse(usuarioId)
            };

            // Si se pasó un planId o servicioId, seleccionamos automáticamente ese valor
            if (planId.HasValue)
            {
                var planSeleccionado = planes.FirstOrDefault(p => p.PlanId == planId.Value);
                viewModel.PlanId = planSeleccionado?.PlanId ?? 0;
            }

            if (servicioId.HasValue)
            {
                var servicioSeleccionado = serviciosIndividuales.FirstOrDefault(s => s.ServicioId == servicioId.Value);
                viewModel.ServicioIndividualId = servicioSeleccionado?.ServicioId ?? 0;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(int? planId, string servicioIds)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Index", "Login");
            }

            // Obtener los planes y servicios
            var planes = await _planesService.ObtenerTodosLosPlanesAsync();
            var serviciosIndividuales = await _serviciosService.ObtenerServiciosIndividualesAsync();

            var planViewModels = planes.Select(plan => new PlanViewModel
            {
                PlanId = plan.PlanId,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                Imagen = plan.Imagen,
                ServiciosIncluidos = plan.PlanServicios.Select(ps => ps.Servicio.Nombre).ToList(),
                ServiciosAdicionales = plan.ServiciosAdicionales.Select(sa => new ServicioAdicionalViewModel
                {
                    ServicioId = sa.Servicio.ServicioId,
                    NombreServicio = sa.Servicio.Nombre,
                    PrecioOriginal = sa.Servicio.Precio,
                    PrecioConDescuento = sa.Servicio.Precio - (sa.Servicio.Precio * sa.Descuento),
                    Descuento = sa.Descuento
                }).ToList()
            }).ToList();


            var servicioViewModels = serviciosIndividuales.Select(s => new ServicioViewModel
            {
                ServicioId = s.ServicioId,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                Precio = s.Precio,
                ImagenUrl = s.Imagen
            }).ToList();

            var viewModel = new ContratacionViewModel
            {
                Planes = planViewModels,
                ServiciosIndividualesDisponibles = servicioViewModels,
                UsuarioId = int.Parse(usuarioId)
            };

            // Si se pasó un planId, seleccionamos ese valor
            if (planId.HasValue)
            {
                var planSeleccionado = planes.FirstOrDefault(p => p.PlanId == planId.Value);
                viewModel.PlanId = planSeleccionado?.PlanId ?? 0;
            }

            // Si se pasó un servicioIds, los seleccionamos como servicios adicionales
            if (!string.IsNullOrEmpty(servicioIds))
            {
                var servicioIdsList = servicioIds.Split(',').Select(int.Parse).ToList();
                viewModel.ServiciosAdicionalesIds = servicioIdsList;
            }

            return View(viewModel);
        }


        public async Task<IActionResult> Detalle(int contratacionId)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Index", "Login");
            }

            var resultado = await _contratacionService.ObtenerDetalleContratacionAsync(contratacionId);
            if (resultado.Success)
            {
                var viewModel = new DetalleContratacionViewModel
                {
                    ContratacionId = resultado.Contratacion.ContratacionId,
                    Estado = resultado.Contratacion.Estado,
                    FechaContratacion = resultado.Contratacion.FechaContratacion,
                    NombrePlan = resultado.Contratacion.Plan?.Nombre,
                    PrecioPlan = resultado.Contratacion.Plan?.Precio ?? 0,
                    ServiciosAdicionalesContratados = resultado.Contratacion.ServiciosAdicionalesContratados.Select(sa => new ServicioAdicionalViewModel
                    {
                        NombreServicio = sa.ServicioAdicional.Servicio.Nombre,
                        PrecioOriginal = sa.ServicioAdicional.Servicio.Precio,
                        PrecioConDescuento = sa.ServicioAdicional.Servicio.Precio - (sa.ServicioAdicional.Servicio.Precio * sa.ServicioAdicional.Descuento),
                        Descuento = sa.ServicioAdicional.Descuento
                    }).ToList(),
                    NombreTecnico = resultado.Contratacion.Cita?.Tecnico?.Nombre,
                    FotoTecnicoUrl = resultado.Contratacion.Cita?.Tecnico?.Foto,
                    DireccionDomicilioCompleta = resultado.Contratacion.Cita?.Direccion?.Colonia,
                    PoliticaCancelacion = "Política de cancelación detallada"
                };

                return View(viewModel);
            }

            ModelState.AddModelError("", "No se encontró la contratación solicitada.");
            return RedirectToAction("Index");
        }
    }
}