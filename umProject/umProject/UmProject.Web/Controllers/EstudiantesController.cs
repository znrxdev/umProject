using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Estudiantes";
            ViewData["Subtitle"] = "Gestión de estudiantes";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var estudiantes = await _estudianteService.ListarEstudiantesAsync(idSesion);
            return View(estudiantes);
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles del Estudiante";
            ViewData["Subtitle"] = "Información completa del estudiante";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var estudianteDetalle = await _estudianteService.ObtenerEstudianteDetalleAsync(id, idSesion);
            if (estudianteDetalle == null)
            {
                TempData["ErrorMessage"] = "Estudiante no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(estudianteDetalle);
        }

        // GET: Estudiantes/Inscripciones/5
        public async Task<IActionResult> Inscripciones(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var inscripciones = await _estudianteService.ObtenerInscripcionesAsync(id, idSesion);
            return PartialView("_Inscripciones", inscripciones);
        }

        // GET: Estudiantes/Grupos/5
        public async Task<IActionResult> Grupos(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var grupos = await _estudianteService.ObtenerGruposAsync(id, idSesion);
            return PartialView("_Grupos", grupos);
        }

        // GET: Estudiantes/Secciones/5
        public async Task<IActionResult> Secciones(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var secciones = await _estudianteService.ObtenerSeccionesAsync(id, idSesion);
            return PartialView("_Secciones", secciones);
        }

        // GET: Estudiantes/Periodos/5
        public async Task<IActionResult> Periodos(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var periodos = await _estudianteService.ObtenerPeriodosAsync(id, idSesion);
            return PartialView("_Periodos", periodos);
        }

        // GET: Estudiantes/Evaluaciones/5
        public async Task<IActionResult> Evaluaciones(int id, bool? soloActuales)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var evaluaciones = await _estudianteService.ObtenerEvaluacionesAsync(id, idSesion, soloActuales);
            return PartialView("_Evaluaciones", evaluaciones);
        }

        // GET: Estudiantes/Desempeno/5
        public async Task<IActionResult> Desempeno(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var desempeno = await _estudianteService.ObtenerDesempenoPorPeriodoAsync(id, idSesion);
            return PartialView("_Desempeno", desempeno);
        }

        // GET: Estudiantes/Sanciones/5
        public async Task<IActionResult> Sanciones(int id, bool? soloActivas)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var sanciones = await _estudianteService.ObtenerSancionesAsync(id, idSesion, soloActivas);
            return PartialView("_Sanciones", sanciones);
        }

        // GET: Estudiantes/SolicitudesBecas/5
        public async Task<IActionResult> SolicitudesBecas(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var solicitudes = await _estudianteService.ObtenerSolicitudesBecasAsync(id, idSesion);
            return PartialView("_SolicitudesBecas", solicitudes);
        }
    }
}

