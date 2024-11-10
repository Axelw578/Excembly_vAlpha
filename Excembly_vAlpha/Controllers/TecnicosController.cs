using Excembly_vAlpha.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Excembly_vAlpha.ViewModels;

namespace Excembly_vAlpha.Controllers
{
    public class TecnicosController : Controller
    {
        private readonly TecnicosService _tecnicoService;

        public TecnicosController(TecnicosService tecnicoService)
        {
            _tecnicoService = tecnicoService;
        }

        public IActionResult Index()
        {
            // Obtiene la lista de técnicos desde el servicio
            List<TecnicoViewModel> tecnicos = _tecnicoService.ObtenerTodosLosTecnicos();

            // Si no hay técnicos en la base de datos, crea datos ficticios
            if (tecnicos == null || tecnicos.Count == 0)
            {
                tecnicos = new List<TecnicoViewModel>
        {
            new TecnicoViewModel
            {
                Nombre = "Carlos",
                Apellidos = "Ramírez",
                Foto = "/images/tecnico1.jpg",
                Experiencia = "5 años",
                Edad = 30,
                Disponibilidad = true,
                FechaRegistro = "2024-01-15"
            },
            new TecnicoViewModel
            {
                Nombre = "Ana",
                Apellidos = "López",
                Foto = "/images/tecnico2.jpg",
                Experiencia = "3 años",
                Edad = 28,
                Disponibilidad = false,
                FechaRegistro = "2023-11-01"
            }
        };
            }

            return View(tecnicos);
        }

    }
}
