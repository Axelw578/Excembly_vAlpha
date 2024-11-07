﻿using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Excembly_vAlpha.Controllers
{
    public class PlanesController : Controller
    {
        private readonly PlanesService _planesService;

        public PlanesController(PlanesService planesService)
        {
            _planesService = planesService;
        }

        // Acción para mostrar todos los planes
        public ActionResult Index()
        {
            // Obtener los planes desde el servicio
            var planes = _planesService.ObtenerTodosLosPlanes();

            // Convertir los datos de los planes en una lista de PlanViewModel
            var planesViewModel = planes.Select(plan => new PlanViewModel
            {
                PlanId = plan.PlanId,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                Imagen = plan.Imagen,

                // Convertir los servicios incluidos en el plan
                ServiciosIncluidos = plan.PlanServicios?.Select(ps => ps.Servicio.Nombre).ToList() ?? new List<string>(),

                // Convertir los servicios adicionales con descuento en un nuevo ViewModel
                ServiciosAdicionales = plan.ServiciosAdicionales?.Select(sa => new ServicioAdicionalViewModel
                {
                    NombreServicio = sa.Servicio.Nombre,
                    PrecioOriginal = sa.Servicio.Precio,
                    PrecioConDescuento = sa.Servicio.Precio * (1 - sa.Descuento), // Calculando el precio con descuento
                    Descuento = sa.Descuento
                }).ToList() ?? new List<ServicioAdicionalViewModel>()
            }).ToList();

            // Pasar el modelo a la vista
            return View(planesViewModel);
        }
    }
}
