using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using UmProject.Web.Services;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class ReportesController : Controller
    {
        private readonly IReporteService _reporteService;
        private readonly IPdfService _pdfService;

        public ReportesController(IReporteService reporteService, IPdfService pdfService)
        {
            _reporteService = reporteService;
            _pdfService = pdfService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Reportes";
            ViewData["Subtitle"] = "Generación de reportes del sistema";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GenerarReporte(string tipoReporte, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Parsear fechas desde query string si vienen como string
            if (!fechaInicio.HasValue && Request.Query.ContainsKey("fechaInicio"))
            {
                if (DateTime.TryParse(Request.Query["fechaInicio"], out var fechaInicioParsed))
                {
                    fechaInicio = fechaInicioParsed;
                }
            }
            
            if (!fechaFin.HasValue && Request.Query.ContainsKey("fechaFin"))
            {
                if (DateTime.TryParse(Request.Query["fechaFin"], out var fechaFinParsed))
                {
                    fechaFin = fechaFinParsed;
                }
            }

            ViewBag.TipoReporte = tipoReporte;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;

            switch (tipoReporte?.ToLower())
            {
                case "usuarios-activos":
                    var usuariosActivos = await _reporteService.GenerarReporteUsuariosAsync(idSesion.Value, 151, fechaInicio, fechaFin);
                    return View("ReporteUsuarios", usuariosActivos);
                
                case "usuarios-inactivos":
                    var usuariosInactivos = await _reporteService.GenerarReporteUsuariosAsync(idSesion.Value, 152, fechaInicio, fechaFin);
                    return View("ReporteUsuarios", usuariosInactivos);
                
                case "personas":
                    var personas = await _reporteService.GenerarReportePersonasAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReportePersonas", personas);
                
                case "materias":
                    var materias = await _reporteService.GenerarReporteMateriasAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteMaterias", materias);
                
                case "periodos":
                    var periodos = await _reporteService.GenerarReportePeriodosAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReportePeriodos", periodos);
                
                case "secciones":
                    var secciones = await _reporteService.GenerarReporteSeccionesAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteSecciones", secciones);
                
                case "grupos":
                    var grupos = await _reporteService.GenerarReporteGruposAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteGrupos", grupos);
                
                case "inscripciones":
                    var inscripciones = await _reporteService.GenerarReporteInscripcionesAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteInscripciones", inscripciones);
                
                case "evaluaciones":
                    var evaluaciones = await _reporteService.GenerarReporteEvaluacionesAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteEvaluaciones", evaluaciones);
                
                case "becas-programas":
                    var becasProgramas = await _reporteService.GenerarReporteBecasProgramasAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteBecasProgramas", becasProgramas);
                
                case "becas-convocatorias":
                    var becasConvocatorias = await _reporteService.GenerarReporteBecasConvocatoriasAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteBecasConvocatorias", becasConvocatorias);
                
                case "becas-solicitudes":
                    var becasSolicitudes = await _reporteService.GenerarReporteBecasSolicitudesAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteBecasSolicitudes", becasSolicitudes);
                
                case "sanciones":
                    var sanciones = await _reporteService.GenerarReporteSancionesAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteSanciones", sanciones);
                
                case "transacciones":
                    var transacciones = await _reporteService.GenerarReporteTransaccionesAsync(idSesion.Value, fechaInicio, fechaFin);
                    return View("ReporteTransacciones", transacciones);
                
                default:
                    TempData["ErrorMessage"] = "Tipo de reporte no válido.";
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExportarPdf(string tipoReporte, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                byte[] pdfBytes = tipoReporte?.ToLower() switch
                {
                    "usuarios-activos" => await _pdfService.GenerarPdfUsuariosAsync(
                        await _reporteService.GenerarReporteUsuariosAsync(idSesion.Value, 151, fechaInicio, fechaFin),
                        "REPORTE DE USUARIOS ACTIVOS",
                        fechaInicio,
                        fechaFin),
                    
                    "usuarios-inactivos" => await _pdfService.GenerarPdfUsuariosAsync(
                        await _reporteService.GenerarReporteUsuariosAsync(idSesion.Value, 152, fechaInicio, fechaFin),
                        "REPORTE DE USUARIOS INACTIVOS",
                        fechaInicio,
                        fechaFin),
                    
                    "personas" => await _pdfService.GenerarPdfPersonasAsync(
                        await _reporteService.GenerarReportePersonasAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "materias" => await _pdfService.GenerarPdfMateriasAsync(
                        await _reporteService.GenerarReporteMateriasAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "periodos" => await _pdfService.GenerarPdfPeriodosAsync(
                        await _reporteService.GenerarReportePeriodosAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "secciones" => await _pdfService.GenerarPdfSeccionesAsync(
                        await _reporteService.GenerarReporteSeccionesAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "grupos" => await _pdfService.GenerarPdfGruposAsync(
                        await _reporteService.GenerarReporteGruposAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "inscripciones" => await _pdfService.GenerarPdfInscripcionesAsync(
                        await _reporteService.GenerarReporteInscripcionesAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "evaluaciones" => await _pdfService.GenerarPdfEvaluacionesAsync(
                        await _reporteService.GenerarReporteEvaluacionesAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "becas-programas" => await _pdfService.GenerarPdfBecasProgramasAsync(
                        await _reporteService.GenerarReporteBecasProgramasAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "becas-convocatorias" => await _pdfService.GenerarPdfBecasConvocatoriasAsync(
                        await _reporteService.GenerarReporteBecasConvocatoriasAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "becas-solicitudes" => await _pdfService.GenerarPdfBecasSolicitudesAsync(
                        await _reporteService.GenerarReporteBecasSolicitudesAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "sanciones" => await _pdfService.GenerarPdfSancionesAsync(
                        await _reporteService.GenerarReporteSancionesAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    "transacciones" => await _pdfService.GenerarPdfTransaccionesAsync(
                        await _reporteService.GenerarReporteTransaccionesAsync(idSesion.Value, fechaInicio, fechaFin),
                        fechaInicio,
                        fechaFin),
                    
                    _ => throw new ArgumentException("Tipo de reporte no válido")
                };

                var nombreArchivo = $"Reporte_{tipoReporte}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                return File(pdfBytes, "application/pdf", nombreArchivo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al generar PDF: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

