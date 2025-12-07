using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class AuditoriaController : Controller
    {
        private readonly ITransaccionService _transaccionService;
        private readonly IErrorSqlService _errorSqlService;

        public AuditoriaController(ITransaccionService transaccionService, IErrorSqlService errorSqlService)
        {
            _transaccionService = transaccionService;
            _errorSqlService = errorSqlService;
        }

        public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin, string? tab = "transacciones")
        {
            ViewData["Title"] = "Auditoría";
            ViewData["Subtitle"] = "Registro de transacciones y errores del sistema";

            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.Tab = tab;

            if (tab == "errores")
            {
                var errores = await _errorSqlService.ListarErroresAsync(idSesion.Value, fechaInicio, fechaFin);
                return View("Errores", errores);
            }

            var transacciones = await _transaccionService.ListarAuditoriaAsync(idSesion.Value, fechaInicio, fechaFin);
            return View(transacciones);
        }
    }
}

