using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class SancionesAcademicasController : Controller
    {
        private readonly ISancionAcademicaService _sancionAcademicaService;
        private readonly IEstudianteService _estudianteService;
        private readonly IEstadoService _estadoService;
        private readonly ICatalogoService _catalogoService;
        private readonly IUsuarioService _usuarioService;

        public SancionesAcademicasController(
            ISancionAcademicaService sancionAcademicaService,
            IEstudianteService estudianteService,
            IEstadoService estadoService,
            ICatalogoService catalogoService,
            IUsuarioService usuarioService)
        {
            _sancionAcademicaService = sancionAcademicaService;
            _estudianteService = estudianteService;
            _estadoService = estadoService;
            _catalogoService = catalogoService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Sanciones Académicas";
            ViewData["Subtitle"] = "Administración de sanciones académicas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var sanciones = await _sancionAcademicaService.ListarSancionesAcademicasAsync(idSesion.Value);
                return View(sanciones ?? new List<SancionAcademica>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<SancionAcademica>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Sanción Académica";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var sancion = await _sancionAcademicaService.ObtenerSancionAcademicaPorIdAsync(id, idSesion);
            if (sancion == null)
            {
                TempData["ErrorMessage"] = "Sanción académica no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(sancion);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nueva Sanción Académica";
            ViewData["Subtitle"] = "Registrar nueva sanción académica";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 87); // 87 = AGREGAR SANCIÓN
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SancionAcademica sancionAcademica)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (ModelState.IsValid)
            {
                sancionAcademica.EsApelable = sancionAcademica.EsApelable;
                sancionAcademica.FechaRegistro = sancionAcademica.FechaRegistro ?? DateTime.Now;

                var resultado = await _sancionAcademicaService.AgregarSancionAcademicaAsync(sancionAcademica, idSesion);

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

            ViewData["Title"] = "Nueva Sanción Académica";
            ViewData["Subtitle"] = "Registrar nueva sanción académica";
            await CargarCatalogos(idSesion, 87);
            return View(sancionAcademica);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Sanción Académica";
            ViewData["Subtitle"] = "Modificar información de sanción académica";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var sancion = await _sancionAcademicaService.ObtenerSancionAcademicaPorIdAsync(id, idSesion);
            if (sancion == null)
            {
                TempData["ErrorMessage"] = "Sanción académica no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCatalogos(idSesion, 90); // 90 = ACTUALIZAR SANCIÓN
            return View(sancion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SancionAcademica sancionAcademica)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != sancionAcademica.IdSancion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _sancionAcademicaService.ActualizarSancionAcademicaAsync(sancionAcademica, idSesion);

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

            ViewData["Title"] = "Editar Sanción Académica";
            ViewData["Subtitle"] = "Modificar información de sanción académica";
            await CargarCatalogos(idSesion, 90);
            return View(sancionAcademica);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar Estudiantes
            var estudiantes = await _estudianteService.ListarEstudiantesAsync(idSesion);
            ViewBag.Estudiantes = estudiantes ?? new List<Estudiante>();

            // Cargar Tipos de Sanción (Id_Tipo_Catalogo = 6)
            var tiposSancion = await _catalogoService.ListarCatalogosPorTipoAsync(6, idSesion);
            ViewBag.TiposSancion = tiposSancion ?? new List<Catalogo>();

            // Cargar Tipos de Falta (Id_Tipo_Catalogo = 24)
            // NOTA: Algunos registros pueden tener valores de Severidad (Id_Tipo_Catalogo = 7) en este campo
            var tiposFalta = await _catalogoService.ListarCatalogosPorTipoAsync(24, idSesion);
            var severidadesParaFalta = await _catalogoService.ListarCatalogosPorTipoAsync(7, idSesion);
            // Combinar ambos para manejar datos inconsistentes
            var todosTiposFalta = new List<Catalogo>();
            if (tiposFalta != null) todosTiposFalta.AddRange(tiposFalta);
            if (severidadesParaFalta != null) todosTiposFalta.AddRange(severidadesParaFalta);
            ViewBag.TiposFalta = todosTiposFalta;

            // Cargar Severidades (Id_Tipo_Catalogo = 7)
            // NOTA: Algunos registros pueden tener valores de Tipo de Falta (Id_Tipo_Catalogo = 24) en este campo
            var severidades = await _catalogoService.ListarCatalogosPorTipoAsync(7, idSesion);
            var tiposFaltaParaSeveridad = await _catalogoService.ListarCatalogosPorTipoAsync(24, idSesion);
            // Combinar ambos para manejar datos inconsistentes
            var todasSeveridades = new List<Catalogo>();
            if (severidades != null) todasSeveridades.AddRange(severidades);
            if (tiposFaltaParaSeveridad != null) todasSeveridades.AddRange(tiposFaltaParaSeveridad);
            ViewBag.Severidades = todasSeveridades;

            // Cargar Estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();

            // Cargar Usuarios para Resolución
            var usuarios = await _usuarioService.ListarUsuariosAsync(idSesion);
            ViewBag.UsuariosResolucion = usuarios ?? new List<Usuario>();
        }
    }
}

