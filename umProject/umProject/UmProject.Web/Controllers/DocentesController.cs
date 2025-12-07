using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class DocentesController : Controller
    {
        private readonly IDocenteService _docenteService;

        public DocentesController(IDocenteService docenteService)
        {
            _docenteService = docenteService;
        }

        // GET: Docentes
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Docentes";
            ViewData["Subtitle"] = "Gestión de docentes";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var docentes = await _docenteService.ListarDocentesAsync(idSesion);
            return View(docentes);
        }

        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles del Docente";
            ViewData["Subtitle"] = "Información completa del docente";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var docenteDetalle = await _docenteService.ObtenerDocenteDetalleAsync(id, idSesion);
            if (docenteDetalle == null)
            {
                TempData["ErrorMessage"] = "Docente no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(docenteDetalle);
        }

        // GET: Docentes/Evaluaciones/5
        public async Task<IActionResult> Evaluaciones(int id, int? idPeriodo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var evaluaciones = await _docenteService.ObtenerEvaluacionesRealizadasAsync(id, idSesion, idPeriodo);
            return PartialView("_Evaluaciones", evaluaciones);
        }

        // GET: Docentes/DetalleEvaluacion/5
        public async Task<IActionResult> DetalleEvaluacion(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var detalle = await _docenteService.ObtenerDetalleEvaluacionAsync(id, idSesion);
            if (detalle == null)
            {
                return Json(new { success = false, message = "Evaluación no encontrada" });
            }

            return PartialView("_DetalleEvaluacion", detalle);
        }

        // GET: Docentes/Secciones/5
        public async Task<IActionResult> Secciones(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var secciones = await _docenteService.ObtenerSeccionesAsignadasAsync(id, idSesion);
            return PartialView("_Secciones", secciones);
        }

        // GET: Docentes/EstudiantesSeccion/5
        public async Task<IActionResult> EstudiantesSeccion(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var estudiantes = await _docenteService.ObtenerEstudiantesSeccionAsync(id, idSesion);
            return PartialView("_EstudiantesSeccion", estudiantes);
        }
    }
}

