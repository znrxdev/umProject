using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class RevisionEvaluacionesController : Controller
    {
        private readonly IEvaluacionAlumnoService _evaluacionAlumnoService;
        private readonly IEvaluacionInstanciaService _evaluacionInstanciaService;
        private readonly IEstadoService _estadoService;

        public RevisionEvaluacionesController(
            IEvaluacionAlumnoService evaluacionAlumnoService,
            IEvaluacionInstanciaService evaluacionInstanciaService,
            IEstadoService estadoService)
        {
            _evaluacionAlumnoService = evaluacionAlumnoService;
            _evaluacionInstanciaService = evaluacionInstanciaService;
            _estadoService = estadoService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Revisión de Evaluaciones";
            ViewData["Subtitle"] = "Evaluaciones pendientes de revisión";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Obtener todas las evaluaciones
                var todasEvaluaciones = await _evaluacionAlumnoService.ListarEvaluacionesAlumnoAsync(idSesion.Value);
                
                // Obtener estado "EN REVISION"
                var estadoEnRevision = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(128, idSesion.Value);
                var idEstadoEnRevision = estadoEnRevision?.FirstOrDefault(e => e.NombreEstado?.ToUpper() == "EN REVISION")?.IdEstado;
                
                // Filtrar evaluaciones que requieren revisión:
                // Debe cumplir AMBAS condiciones:
                // 1. Estado = "EN REVISION"
                // 2. RequiereRevisionInterna = 1 en la instancia
                var evaluacionesRevision = new List<EvaluacionAlumno>();
                
                if (todasEvaluaciones != null && idEstadoEnRevision.HasValue)
                {
                    foreach (var evaluacion in todasEvaluaciones)
                    {
                        // Verificar que el estado sea "EN REVISION"
                        if (evaluacion.IdEstado == idEstadoEnRevision.Value && evaluacion.IdEvaluacionInstancia.HasValue)
                        {
                            // Obtener la instancia para verificar RequiereRevisionInterna
                            var instancia = await _evaluacionInstanciaService.ObtenerEvaluacionInstanciaPorIdAsync(
                                evaluacion.IdEvaluacionInstancia.Value, idSesion.Value);
                            
                            // Solo agregar si RequiereRevisionInterna = true
                            if (instancia != null && instancia.RequiereRevisionInterna)
                            {
                                evaluacionesRevision.Add(evaluacion);
                            }
                        }
                    }
                }

                return View(evaluacionesRevision);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<EvaluacionAlumno>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Evaluación";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var evaluacion = await _evaluacionAlumnoService.ObtenerEvaluacionAlumnoPorIdAsync(id, idSesion);
            if (evaluacion == null)
            {
                TempData["ErrorMessage"] = "Evaluación no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            
            // Obtener estados disponibles para aprobar
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(128, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();
            
            return View(evaluacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprobar(int id, string fechaPublicacion, string motivoAjuste)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            try
            {
                var evaluacion = await _evaluacionAlumnoService.ObtenerEvaluacionAlumnoPorIdAsync(id, idSesion);
                if (evaluacion == null)
                {
                    TempData["ErrorMessage"] = "Evaluación no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                // Validar y parsear la fecha (se permite cualquier fecha; la validación de rango se hará en SP)
                if (string.IsNullOrWhiteSpace(fechaPublicacion) || !DateTime.TryParse(fechaPublicacion, out DateTime fechaPub))
                {
                    TempData["ErrorMessage"] = "Debe proporcionar una fecha de publicación válida.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                // Motivo de ajuste requerido al aprobar
                if (string.IsNullOrWhiteSpace(motivoAjuste))
                {
                    TempData["ErrorMessage"] = "Debe ingresar un motivo de ajuste.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                // Obtener estado "ACTIVO" o "APROBADO"
                var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(128, idSesion);
                var estadoActivo = estados?.FirstOrDefault(e => e.NombreEstado?.ToUpper() == "ACTIVO");
                
                if (estadoActivo == null)
                {
                    TempData["ErrorMessage"] = "No se encontró el estado ACTIVO.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                // Actualizar la evaluación
                evaluacion.IdUsuarioValidador = idSesion; // El usuario actual es el validador
                evaluacion.FechaValidacion = DateTime.Now;
                evaluacion.FechaPublicacion = fechaPub;
                evaluacion.IdEstado = estadoActivo.IdEstado;
                evaluacion.MotivoAjuste = motivoAjuste;

                var resultado = await _evaluacionAlumnoService.ActualizarEvaluacionAlumnoAsync(evaluacion, idSesion);

                if (resultado.Codigo != -1)
                {
                    TempData["SuccessMessage"] = "Evaluación aprobada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Details), new { id });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al aprobar la evaluación: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rechazar(int id, string motivoAjuste)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            try
            {
                var evaluacion = await _evaluacionAlumnoService.ObtenerEvaluacionAlumnoPorIdAsync(id, idSesion);
                if (evaluacion == null)
                {
                    TempData["ErrorMessage"] = "Evaluación no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                if (string.IsNullOrWhiteSpace(motivoAjuste))
                {
                    TempData["ErrorMessage"] = "Debe proporcionar un motivo de ajuste.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                // Actualizar la evaluación con el motivo de ajuste
                evaluacion.MotivoAjuste = motivoAjuste;
                // El estado se mantiene en "EN REVISION" o se puede cambiar según la lógica del negocio

                var resultado = await _evaluacionAlumnoService.ActualizarEvaluacionAlumnoAsync(evaluacion, idSesion);

                if (resultado.Codigo != -1)
                {
                    TempData["SuccessMessage"] = "Evaluación rechazada. El motivo de ajuste ha sido registrado.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Details), new { id });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al rechazar la evaluación: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}

