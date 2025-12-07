using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class MiHistorialController : Controller
    {
        private readonly ISancionAcademicaService _sancionAcademicaService;
        private readonly IEstudianteService _estudianteService;

        public MiHistorialController(
            ISancionAcademicaService sancionAcademicaService,
            IEstudianteService estudianteService)
        {
            _sancionAcademicaService = sancionAcademicaService;
            _estudianteService = estudianteService;
        }

        // GET: MiHistorial
        public async Task<IActionResult> Index(string tab = "sanciones")
        {
            ViewData["Title"] = "Mi Historial";
            ViewData["Subtitle"] = "Consulta tu historial académico";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.ActiveTab = tab;

            // Cargar datos según el tab activo
            if (tab == "sanciones")
            {
                try
                {
                    var sanciones = await _sancionAcademicaService.ObtenerMisSancionesAcademicasAsync(idSesion.Value);
                    ViewBag.Sanciones = sanciones;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al cargar sanciones: {ex.Message}";
                    ViewBag.Sanciones = new List<UmProject.Entities.SancionAcademica>();
                }
            }
            else if (tab == "evaluaciones")
            {
                try
                {
                    var evaluaciones = await _estudianteService.ObtenerEvaluacionesAsync(idSesion.Value, idSesion);
                    var evaluacionesPublicadas = evaluaciones?
                        .Where(e => e.FechaPublicacion.HasValue && e.FechaPublicacion.Value <= DateTime.Now)
                        .OrderByDescending(e => e.FechaPublicacion)
                        .ToList() ?? new List<UmProject.Entities.EstudianteEvaluacion>();

                    ViewBag.Evaluaciones = evaluacionesPublicadas;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al cargar evaluaciones: {ex.Message}";
                    ViewBag.Evaluaciones = new List<UmProject.Entities.EstudianteEvaluacion>();
                }
            }
            else if (tab == "becas")
            {
                // TODO: Implementar cuando se tenga el servicio de becas
                ViewBag.Becas = new List<object>();
            }

            return View();
        }

        // POST: MiHistorial/ApelarSancion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApelarSancion(int idSancion, string observacionesApelacion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrWhiteSpace(observacionesApelacion))
            {
                TempData["ErrorMessage"] = "Debe ingresar los comentarios de apelación.";
                return RedirectToAction("Index", new { tab = "sanciones" });
            }

            try
            {
                var resultado = await _sancionAcademicaService.ApelarSancionAcademicaAsync(idSancion, observacionesApelacion, idSesion.Value);
                
                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al procesar la apelación: {ex.Message}";
            }

            return RedirectToAction("Index", new { tab = "sanciones" });
        }
    }
}

