using Excembly_vAlpha.Services;
using Excembly_vAlpha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Excembly_vAlpha.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        // Acción para la vista principal de la empresa (Index)
        public async Task<IActionResult> Index()
        {
            // Obtener la información de la empresa a través del servicio
            var empresaData = await _empresaService.InformacionEmpresa();

            // Mapear los datos a EmpresaViewModel
            var empresaViewModel = new EmpresaViewModel
            {
                MisionVision = empresaData.MisionVision,
                HorarioTienda = empresaData.HorarioTienda,
                DomicilioTienda = empresaData.DomicilioTienda,
                Aclaracion = empresaData.Aclaracion,
                PoliticaCancelacion = empresaData.PoliticaCancelacion, // Incluyendo PoliticaCancelacion
                ImagenMapa = empresaData.ImagenMapa,

            };

            return View("Views/Empresa/Index.cshtml", empresaViewModel);
        }

        // Acción para la vista de reseñas (Reseñas)
        public async Task<IActionResult> Reseñas()
        {
            // Obtener las reseñas de usuarios a través del servicio
            var citas = await _empresaService.ReseñasUsuarios();

            // Mapear las citas a una lista de ReseñasViewModel
            var reseñasViewModel = citas.Select(c => new ReseñasViewModel
            {
                NombreUsuario = c.Usuario.Nombre,
                Comentarios = c.Comentarios, // Cambié 'Comentario' a 'Comentarios' para que coincida con el nombre de la propiedad en ReseñasViewModel
                FechaCita = c.FechaCita, // Asumiendo que deseas incluir la fecha de la cita
                FotoPerfilUrl = c.Usuario.FotoPerfilUrl // URL de la imagen del usuario
            }).ToList();

            // Crear un EmpresaViewModel que solo contiene la lista de reseñas
            var empresaViewModel = new EmpresaViewModel
            {
                Reseñas = reseñasViewModel
            };

            return View("Views/Empresa/Reseñas.cshtml", empresaViewModel);
        }
    }
}
