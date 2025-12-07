using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using UmProject.Web.Models;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class BecasController : Controller
    {
        private readonly ISolicitudBecaService _solicitudBecaService;
        private readonly IEstudianteService _estudianteService;
        private readonly IEstadoService _estadoService;
        private readonly IUsuarioService _usuarioService;

        public BecasController(
            ISolicitudBecaService solicitudBecaService,
            IEstudianteService estudianteService,
            IEstadoService estadoService,
            IUsuarioService usuarioService)
        {
            _solicitudBecaService = solicitudBecaService;
            _estudianteService = estudianteService;
            _estadoService = estadoService;
            _usuarioService = usuarioService;
        }

        public IActionResult Solicitudes()
        {
            ViewData["Title"] = "Solicitudes de Becas";
            ViewData["Subtitle"] = "Aplicación y seguimiento (estudiante)";
            return RedirectToAction(nameof(SolicitudesBeca));
        }

        [HttpGet]
        public async Task<IActionResult> SolicitudesBeca()
        {
            ViewData["Title"] = "Solicitudes de Becas";
            ViewData["Subtitle"] = "Aplicación y seguimiento (estudiante)";

            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var vm = new SolicitudesBecaViewModel();
            try
            {
                vm.ProgramasDisponibles = await _estudianteService.ObtenerProgramasBecaDisponiblesAsync(idSesion.Value);
                vm.MisSolicitudes = await _estudianteService.ObtenerSolicitudesBecasAsync(idSesion.Value, idSesion.Value);
                vm.HistorialSolicitudes = await _estudianteService.ObtenerHistorialSolicitudesBecaAsync(idSesion.Value, idSesion.Value);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View("Solicitudes", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AplicarSolicitud(int idPrograma, string? observaciones)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var resultado = await _estudianteService.AplicarSolicitudBecaAsync(idPrograma, observaciones, idSesion.Value);
                if (resultado.Codigo != -1)
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
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(SolicitudesBeca));
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Becas";
            ViewData["Subtitle"] = "Administración de solicitudes de becas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var solicitudes = await _solicitudBecaService.ListarSolicitudesBecaAsync(idSesion.Value);
                return View(solicitudes ?? new List<SolicitudBeca>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<SolicitudBeca>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Solicitud de Beca";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var solicitud = await _solicitudBecaService.ObtenerSolicitudBecaPorIdAsync(id, idSesion);
            if (solicitud == null)
            {
                TempData["ErrorMessage"] = "Solicitud de beca no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(solicitud);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nueva Solicitud de Beca";
            ViewData["Subtitle"] = "Registrar nueva solicitud de beca";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 68); // 68 = AGREGAR SOLICITUD BECA
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SolicitudBeca solicitudBeca)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (ModelState.IsValid)
            {
                solicitudBeca.FechaSolicitud = solicitudBeca.FechaSolicitud ?? DateTime.Now;

                var resultado = await _solicitudBecaService.AgregarSolicitudBecaAsync(solicitudBeca, idSesion);

                if (resultado.Codigo != -1)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", resultado.Mensaje);
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Nueva Solicitud de Beca";
            ViewData["Subtitle"] = "Registrar nueva solicitud de beca";
            await CargarCatalogos(idSesion, 68);
            return View(solicitudBeca);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Solicitud de Beca";
            ViewData["Subtitle"] = "Modificar información de solicitud de beca";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var solicitud = await _solicitudBecaService.ObtenerSolicitudBecaPorIdAsync(id, idSesion);
            if (solicitud == null)
            {
                TempData["ErrorMessage"] = "Solicitud de beca no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCatalogos(idSesion, 69); // 69 = ACTUALIZAR SOLICITUD BECA
            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SolicitudBeca solicitudBeca)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != solicitudBeca.IdSolicitudBeca)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _solicitudBecaService.ActualizarSolicitudBecaAsync(solicitudBeca, idSesion);

                if (resultado.Codigo != -1)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", resultado.Mensaje);
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Editar Solicitud de Beca";
            ViewData["Subtitle"] = "Modificar información de solicitud de beca";
            await CargarCatalogos(idSesion, 69);
            return View(solicitudBeca);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar Estudiantes
            var estudiantes = await _estudianteService.ListarEstudiantesAsync(idSesion);
            ViewBag.Estudiantes = estudiantes ?? new List<Estudiante>();

            // TODO: Cargar Programas de Becas (necesitamos un servicio para esto)
            // Por ahora, dejamos vacío
            ViewBag.ProgramasBeca = new List<object>();

            // TODO: Cargar Convocatorias (necesitamos un servicio para esto)
            // Por ahora, dejamos vacío
            ViewBag.Convocatorias = new List<object>();

            // Cargar Estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();

            // Cargar Usuarios Responsables y Supervisores
            var usuarios = await _usuarioService.ListarUsuariosAsync(idSesion);
            ViewBag.Responsables = usuarios ?? new List<Usuario>();
            ViewBag.Supervisores = usuarios ?? new List<Usuario>();
        }
    }
}

