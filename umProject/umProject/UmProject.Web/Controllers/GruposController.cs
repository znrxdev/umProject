using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class GruposController : Controller
    {
        private readonly IGrupoService _grupoService;
        private readonly IPeriodoAcademicoService _periodoAcademicoService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEstadoService _estadoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IInscripcionService _inscripcionService;

        public GruposController(
            IGrupoService grupoService,
            IPeriodoAcademicoService periodoAcademicoService,
            ICatalogoService catalogoService,
            IEstadoService estadoService,
            IUsuarioService usuarioService,
            IInscripcionService inscripcionService)
        {
            _grupoService = grupoService;
            _periodoAcademicoService = periodoAcademicoService;
            _catalogoService = catalogoService;
            _estadoService = estadoService;
            _usuarioService = usuarioService;
            _inscripcionService = inscripcionService;
        }

        public async Task<IActionResult> Index(int? idPeriodo)
        {
            ViewData["Title"] = "Gestión de Grupos";
            ViewData["Subtitle"] = "Administración de grupos académicos";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Cargar períodos académicos para el filtro
                var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion.Value);
                ViewBag.PeriodosAcademicos = periodos ?? new List<PeriodoAcademico>();
                ViewBag.IdPeriodoSeleccionado = idPeriodo;

                var grupos = await _grupoService.ListarGruposAsync(idSesion.Value, idPeriodo);
                return View(grupos ?? new List<Grupo>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<Grupo>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Grupo";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var grupo = await _grupoService.ObtenerGrupoPorIdAsync(id, idSesion);
            if (grupo == null)
            {
                TempData["ErrorMessage"] = "Grupo no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(grupo);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nuevo Grupo";
            ViewData["Subtitle"] = "Registrar nuevo grupo académico";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 101); // 101 = AGREGAR GRUPO
            
            // Establecer estado EN REVISION (4) por defecto
            ViewBag.EstadoRevision = 4;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grupo grupo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            // Remover validaciones de campos que se autogeneran o se fuerzan
            ModelState.Remove(nameof(grupo.CodigoGrupo));
            ModelState.Remove(nameof(grupo.CodigoSeguimiento));
            ModelState.Remove(nameof(grupo.IdEstado));
            ModelState.Remove(nameof(grupo.IdJornada));
            ModelState.Remove(nameof(grupo.Activo));

            if (ModelState.IsValid)
            {
                // Forzar valores según requerimientos
                grupo.IdEstado = 4; // EN REVISION
                grupo.IdJornada = null; // Siempre NULL
                grupo.Activo = true; // Siempre true
                
                // Fecha_Cierre se autocalcula en el stored procedure usando la fecha de inicio del período
                // No es necesario calcularla aquí, pero la dejamos como NULL para que el SP la calcule
                grupo.FechaCierre = null;

                var resultado = await _grupoService.AgregarGrupoAsync(grupo, idSesion);

                if (resultado.Codigo != -1)
                {
                    // El mensaje del SP ya incluye el código generado
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    TempData["CodigoGrupoGenerado"] = resultado.Mensaje; // Guardar para mostrar en modal
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", resultado.Mensaje);
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Nuevo Grupo";
            ViewData["Subtitle"] = "Registrar nuevo grupo académico";
            await CargarCatalogos(idSesion, 101);
            ViewBag.EstadoRevision = 4;
            return View(grupo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Grupo";
            ViewData["Subtitle"] = "Modificar información de grupo";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var grupo = await _grupoService.ObtenerGrupoPorIdAsync(id, idSesion);
            if (grupo == null)
            {
                TempData["ErrorMessage"] = "Grupo no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            grupo.Activo = grupo.Activo ?? true;

            // Limpiar ModelState al cargar la vista (GET) para evitar errores persistentes
            ModelState.Clear();

            await CargarCatalogos(idSesion, 102); // 102 = ACTUALIZAR GRUPO
            return View(grupo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grupo grupo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != grupo.IdGrupo)
            {
                return NotFound();
            }

            // Obtener el grupo actual para mantener valores que no se modifican
            var grupoActual = await _grupoService.ObtenerGrupoPorIdAsync(id, idSesion);
            if (grupoActual == null)
            {
                TempData["ErrorMessage"] = "Grupo no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            
            // Establecer Activo basado en el IdEstado (ACTIVO = 1 significa Activo = true)
            grupo.Activo = grupo.IdEstado == 1;
            
            // Mantener valores que no se pueden modificar
            grupo.CodigoGrupo = grupoActual.CodigoGrupo;
            grupo.CodigoSeguimiento = grupoActual.CodigoSeguimiento;

            // Validar ModelState y mostrar errores si hay
            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}")
                    .ToList();
                
                var mensajeErrores = string.Join(" | ", errores);
                TempData["ErrorMessage"] = string.IsNullOrWhiteSpace(mensajeErrores) ? null : $"Errores de validación: {mensajeErrores}";
                
                ViewData["Title"] = "Editar Grupo";
                ViewData["Subtitle"] = "Modificar información de grupo";
                await CargarCatalogos(idSesion, 102);
                return View(grupo);
            }

            // Validación adicional: pasar de EN REVISION (4) a ACTIVO (1) requiere al menos una inscripción activa
            if (grupoActual.IdEstado == 4 && grupo.IdEstado == 1)
            {
                var inscripciones = await _inscripcionService.ListarInscripcionesGrupoAsync(grupo.IdGrupo ?? 0, idSesion);
                var tieneActiva = inscripciones?.Any(i => i.Activo && i.IdEstado == 1) == true;
                if (!tieneActiva)
                {
                    ModelState.AddModelError("IdEstado", "Para activar el grupo debe tener al menos una inscripción activa.");
                    TempData["ErrorMessage"] = "Para activar el grupo debe tener al menos una inscripción activa.";
                    
                    ViewData["Title"] = "Editar Grupo";
                    ViewData["Subtitle"] = "Modificar información de grupo";
                    await CargarCatalogos(idSesion, 102);
                    return View(grupo);
                }
            }

            // Limpiar errores previos de ModelState antes de procesar
            ModelState.Remove("IdEstado");
            
            var resultado = await _grupoService.ActualizarGrupoAsync(grupo, idSesion);

            if (resultado.Codigo != -1)
            {
                TempData["SuccessMessage"] = resultado.Mensaje;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Agregar el nuevo error solo si la operación falló
                ModelState.AddModelError("IdEstado", resultado.Mensaje);
                TempData["ErrorMessage"] = resultado.Mensaje;
            }

            ViewData["Title"] = "Editar Grupo";
            ViewData["Subtitle"] = "Modificar información de grupo";
            await CargarCatalogos(idSesion, 102);
            
            return View(grupo);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar Períodos Académicos - Solo EN REVISION (4) o PENDIENTE (3) para Create
            var todosPeriodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
            if (idTipoTransaccion == 101) // AGREGAR GRUPO
            {
                ViewBag.Periodos = todosPeriodos?.Where(p => p.IdEstado == 3 || p.IdEstado == 4).ToList() ?? new List<PeriodoAcademico>();
            }
            else
            {
                ViewBag.Periodos = todosPeriodos ?? new List<PeriodoAcademico>();
            }

            // Cargar Tipos de Grupo (Id_Tipo_Catalogo = 17 para TIPO GRUPO)
            ViewBag.TiposGrupo = await _catalogoService.ListarCatalogosPorTipoAsync(17, idSesion);

            // Cargar Coordinadores - Solo usuarios con rol Coordinador Académico (Id_Rol = 4)
            ViewBag.Coordinadores = await _usuarioService.FiltrarUsuariosPorRolAsync(4, idSesion);

            // Estados ya no se cargan para Create - siempre es EN REVISION (4)
            // Solo se cargan para Edit
            if (idTipoTransaccion != 101) // No es AGREGAR GRUPO
            {
                ViewBag.Estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            }
        }

        // GET: Grupos/GetInscripcionesDisponibles
        [HttpGet]
        public async Task<IActionResult> GetInscripcionesDisponibles()
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            try
            {
                var inscripciones = await _inscripcionService.ListarInscripcionesDisponiblesAsync(idSesion.Value);
                if (inscripciones != null && inscripciones.Count > 0)
                {
                    return Json(new { success = true, data = inscripciones });
                }
                else
                {
                    return Json(new { success = true, data = new List<Inscripcion>() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Grupos/GetInscripcionesGrupo/5
        [HttpGet]
        public async Task<IActionResult> GetInscripcionesGrupo(int idGrupo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            try
            {
                var inscripciones = await _inscripcionService.ListarInscripcionesGrupoAsync(idGrupo, idSesion.Value);
                return PartialView("_InscripcionesGrupo", inscripciones);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Grupos/AgregarInscripcionGrupo
        [HttpPost]
        public async Task<IActionResult> AgregarInscripcionGrupo(int idGrupo, int idInscripcion, string? observaciones)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            try
            {
                var resultado = await _inscripcionService.AgregarInscripcionGrupoAsync(idGrupo, idInscripcion, observaciones, idSesion.Value);
                if (resultado.Codigo != -1)
                {
                    return Json(new { success = true, message = resultado.Mensaje });
                }
                else
                {
                    return Json(new { success = false, message = resultado.Mensaje });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
