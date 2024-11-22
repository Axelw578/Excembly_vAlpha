//using Excembly_vAlpha.Models;
//using Excembly_vAlpha.Services;
//using Excembly_vAlpha.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Excembly_vAlpha.ViewModels;
//using Microsoft.AspNetCore.Mvc.Rendering;


//namespace Excembly_vAlpha.Controllers
//{
//    public class CitasController : Controller
//    {
//        private readonly CitasService _citasService;
//        private readonly ContratacionService _contratacionService;
//        private readonly ServicioAdicionalService _servicioAdicionalService;

//        public CitasController(CitasService citasService, ContratacionService contratacionService, ServicioAdicionalService servicioAdicionalService)
//        {
//            _citasService = citasService;
//            _contratacionService = contratacionService;
//            _servicioAdicionalService = servicioAdicionalService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> MisCitas(int usuarioId)
//        {
//            var citasResult = await _citasService.ObtenerCitasPorUsuarioAsync(usuarioId);
//            if (!citasResult.Success || citasResult.Citas == null || !citasResult.Citas.Any())
//            {
//                TempData["InfoMessage"] = citasResult.Message ?? "No tienes citas registradas. ¡Empieza creando una cita!";
//                ViewBag.UsuarioId = usuarioId; // Pasar el usuario para el botón
//                return View(new List<CitaViewModel>());
//            }

//            var citasViewModel = citasResult.Citas.Select(cita => new CitaViewModel
//            {
//                CitaId = cita.CitaId,
//                UsuarioId = cita.UsuarioId,
//                DireccionId = cita.DireccionId,
//                Direccion = cita.Direccion != null
//                    ? $"{cita.Direccion.Calle}, {cita.Direccion.Colonia}, {cita.Direccion.NumeroEdificio}"
//                    : "Sin dirección",
//                FechaCita = cita.FechaCita,
//                EstadoCita = cita.EstadoCita,
//                Comentarios = cita.Comentarios
//            }).ToList();

//            return View(citasViewModel);
//        }



//        // Acción para ver una cita
//        [HttpGet]
//        public async Task<IActionResult> VerCita(int citaId)
//        {
//            var resultadoCita = await _citasService.ObtenerCitaPorIdAsync(citaId);
//            if (!resultadoCita.Success)
//            {
//                TempData["ErrorMessage"] = resultadoCita.Message;
//                return RedirectToAction("MisCitas");
//            }

//            var cita = resultadoCita.Cita;

//            var viewModel = new CrearCitaViewModel
//            {
//                CitaId = cita.CitaId,  // Asegúrate de que esta propiedad exista en la Cita
//                UsuarioId = cita.UsuarioId,
//                DireccionId = cita.DireccionId,
//                TecnicoId = cita.TecnicoId,
//                PlanId = cita.PlanId,
//                ServiciosAdicionales = cita.ServiciosAdicionales.Select(s => s.Id).ToList(),
//                FechaCita = cita.FechaCita,
//                Domicilio = cita.Domicilio,
//                Comentarios = cita.Comentarios
//            };

//            return View("CrearCita", viewModel);
//        }

//        // Acción para crear una nueva cita
//        [HttpGet]
//        public async Task<IActionResult> CrearCita(int usuarioId)
//        {
//            var contratacionesResult = await _contratacionService.On(usuarioId);

//            if (!contratacionesResult.Success || !contratacionesResult.Contrataciones.Any())
//            {
//                TempData["ErrorMessage"] = "No tienes contrataciones activas. Por favor, contrata un servicio antes de agendar una cita.";
//                return RedirectToAction("MisCitas", new { usuarioId });
//            }

//            var nuevaCita = new CrearCitaViewModel
//            {
//                UsuarioId = usuarioId,
//                ContratacionesDisponibles = contratacionesResult.Contrataciones.Select(c => new SelectListItem
//                {
//                    Value = c.ContratacionId.ToString(),
//                    Text = c.Plan.Nombre // O cualquier dato que identifique la contratación
//                }).ToList()
//            };

//            return View(nuevaCita);
//        }


//        // Acción para guardar la cita nueva
//        [HttpPost]
//        public async Task<IActionResult> CrearCita(CrearCitaViewModel nuevaCita)
//        {
//            if (ModelState.IsValid)
//            {
//                var cita = new Cita
//                {
//                    UsuarioId = nuevaCita.UsuarioId,
//                    DireccionId = nuevaCita.DireccionId ?? default(int),
//                    TecnicoId = nuevaCita.TecnicoId ?? default(int),
//                    PlanId = nuevaCita.PlanId ?? default(int),
//                    ServiciosAdicionales = nuevaCita.ServiciosAdicionales.Select(id => new ServicioAdicional { Id = id }).ToList(),
//                    FechaCita = nuevaCita.FechaCita,
//                    Domicilio = nuevaCita.Domicilio,
//                    Comentarios = nuevaCita.Comentarios
//                };

//                var (success, message, citaId) = await _citasService.CrearCitaAsync(cita);
//                if (success)
//                {
//                    TempData["SuccessMessage"] = "Cita creada exitosamente.";
//                    return RedirectToAction("MisCitas", new { usuarioId = nuevaCita.UsuarioId });
//                }
//                TempData["ErrorMessage"] = message;
//            }

//            return View(nuevaCita);
//        }



//        // Acción para editar la cita
//        [HttpPost]
//        public async Task<IActionResult> EditarCita(CrearCitaViewModel citaActualizada)
//        {
//            if (ModelState.IsValid)
//            {
//                var cita = new Cita
//                {
//                    CitaId = citaActualizada.CitaId,  // Si tienes CitaId en el ViewModel
//                    UsuarioId = citaActualizada.UsuarioId,
//                    DireccionId = citaActualizada.DireccionId ?? default(int),
//                    TecnicoId = citaActualizada.TecnicoId ?? default(int),
//                    PlanId = citaActualizada.PlanId ?? default(int),
//                    ServiciosAdicionales = citaActualizada.ServiciosAdicionales.Select(id => new ServicioAdicional { Id = id }).ToList(),
//                    FechaCita = citaActualizada.FechaCita,
//                    Domicilio = citaActualizada.Domicilio,
//                    Comentarios = citaActualizada.Comentarios
//                };

//                var (success, message) = await _citasService.EditarCitaAsync(cita);
//                if (success)
//                {
//                    TempData["SuccessMessage"] = "Cita actualizada exitosamente.";
//                    return RedirectToAction("MisCitas", new { usuarioId = citaActualizada.UsuarioId });
//                }
//                TempData["ErrorMessage"] = message;
//            }

//            return View(citaActualizada);
//        }


//        // Mostrar los detalles de una cita y su contratación asociada
//        [HttpGet]
//        public async Task<IActionResult> DetallesCita(int citaId)
//        {
//            // Obtener la cita con su contratación asociada
//            var citaResult = await _citasService.ObtenerCitaPorIdAsync(citaId);
//            if (!citaResult.Success || citaResult.Cita == null)
//            {
//                TempData["ErrorMessage"] = citaResult.Message ?? "No se encontró la cita especificada.";
//                return RedirectToAction("MisCitas");
//            }

//            var cita = citaResult.Cita;

//            // Cargar detalles adicionales de la contratación si existen
//            Contratacion contratacion = null;
//            if (cita.ContratacionId.HasValue)
//            {
//                var contratacionResult = await _contratacionService.ObtenerDetalleContratacionAsync(cita.ContratacionId.Value);
//                if (contratacionResult.Success)
//                {
//                    contratacion = contratacionResult.Contratacion;
//                }
//            }

//            // Construir el ViewModel para la vista
//            var viewModel = new DetalleCitaViewModel
//            {
//                CitaId = cita.CitaId,
//                UsuarioId = cita.UsuarioId,
//                TecnicoId = cita.TecnicoId,
//                DireccionId = cita.DireccionId,
//                Direccion = cita.Direccion,
//                PlanId = cita.PlanId,
//                Plan = cita.Plan,
//                ServiciosAdicionales = cita.ServiciosAdicionales,
//                FechaCita = cita.FechaCita,
//                FechaCitaModificada = cita.FechaCitaModificada,
//                EstadoCita = cita.EstadoCita,
//                Domicilio = cita.Domicilio,
//                Comentarios = cita.Comentarios,
//                Imagen = cita.Imagen,
//                FechaCancelacion = cita.FechaCancelacion,
//                MotivoCancelacion = cita.MotivoCancelacion,
//                Contratacion = contratacion
//            };

//            return View(viewModel);
//        }
//    }
//}

