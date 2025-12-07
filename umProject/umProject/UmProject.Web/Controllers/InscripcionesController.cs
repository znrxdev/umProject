using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class InscripcionesController : Controller
    {
        private readonly IInscripcionService _inscripcionService;
        private readonly ISeccionService _seccionService;
        private readonly IEstudianteService _estudianteService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEstadoService _estadoService;
        private readonly IUsuarioService _usuarioService;

        public InscripcionesController(
            IInscripcionService inscripcionService,
            ISeccionService seccionService,
            IEstudianteService estudianteService,
            ICatalogoService catalogoService,
            IEstadoService estadoService,
            IUsuarioService usuarioService)
        {
            _inscripcionService = inscripcionService;
            _seccionService = seccionService;
            _estudianteService = estudianteService;
            _catalogoService = catalogoService;
            _estadoService = estadoService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Inscripciones";
            ViewData["Subtitle"] = "Administración de inscripciones académicas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var inscripciones = await _inscripcionService.ListarInscripcionesAsync(idSesion.Value);
                return View(inscripciones ?? new List<Inscripcion>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<Inscripcion>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Inscripción";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var inscripcion = await _inscripcionService.ObtenerInscripcionPorIdAsync(id, idSesion);
            if (inscripcion == null)
            {
                TempData["ErrorMessage"] = "Inscripción no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(inscripcion);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nueva Inscripción";
            ViewData["Subtitle"] = "Registrar nueva inscripción";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 110); // 110 = AGREGAR INSCRIPCIÓN
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inscripcion inscripcion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            // Forzar estado EN REVISION (4) al crear
            inscripcion.IdEstado = 4;
            // El código se autogenera en el SP, asegurar que esté vacío
            inscripcion.CodigoInscripcion = null;
            // No permitir validador, fechas ni motivo al crear
            inscripcion.IdUsuarioValidador = null;
            inscripcion.FechaValidacion = null;
            inscripcion.FechaRetiro = null;
            inscripcion.MotivoRetiro = null;

            if (ModelState.IsValid)
            {
                var resultado = await _inscripcionService.AgregarInscripcionAsync(inscripcion, idSesion);

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

            ViewData["Title"] = "Nueva Inscripción";
            ViewData["Subtitle"] = "Registrar nueva inscripción";
            await CargarCatalogos(idSesion, 110);
            return View(inscripcion);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Inscripción";
            ViewData["Subtitle"] = "Modificar información de inscripción";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var inscripcion = await _inscripcionService.ObtenerInscripcionPorIdAsync(id, idSesion);
            if (inscripcion == null)
            {
                TempData["ErrorMessage"] = "Inscripción no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            
            // Si el estado actual es EN REVISION (4), establecer validador y fecha de validación
            if (inscripcion.IdEstado == 4)
            {
                // El validador es el usuario en sesión
                inscripcion.IdUsuarioValidador = idSesion;
                // Establecer fecha de validación a hoy si no está establecida
                if (!inscripcion.FechaValidacion.HasValue)
                {
                    inscripcion.FechaValidacion = DateTime.Now;
                }
            }

            await CargarCatalogos(idSesion, 111); // 111 = ACTUALIZAR INSCRIPCIÓN
            ViewBag.EsEstadoRevision = inscripcion.IdEstado == 4;
            ViewBag.IdUsuarioSesion = idSesion;
            return View(inscripcion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inscripcion inscripcion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != inscripcion.IdInscripcion)
            {
                return NotFound();
            }

            // Obtener inscripción actual para validaciones
            var inscripcionActual = await _inscripcionService.ObtenerInscripcionPorIdAsync(id, idSesion);

            // No permitir cambiar el estudiante
            if (inscripcionActual != null)
            {
                inscripcion.IdEstudiante = inscripcionActual.IdEstudiante;
            }

            // Validaciones específicas
            if (inscripcionActual != null && inscripcionActual.IdEstado == 4)
            {
                // Si estaba en REVISION y se cambia el estado, establecer validador y fecha
                if (inscripcion.IdEstado != 4)
                {
                    inscripcion.IdUsuarioValidador = idSesion;
                    // Establecer fecha de validación a la fecha actual
                    inscripcion.FechaValidacion = DateTime.Now;
                }
                else
                {
                    // Si sigue en REVISION, mantener validador y establecer fecha de validación a la actual
                    inscripcion.IdUsuarioValidador = idSesion;
                    inscripcion.FechaValidacion = DateTime.Now;
                }
            }

            // Validar fecha de retiro no mayor a la actual
            if (inscripcion.FechaRetiro.HasValue && inscripcion.FechaRetiro.Value > DateTime.Now)
            {
                ModelState.AddModelError("FechaRetiro", "La fecha de retiro no puede ser mayor a la fecha actual.");
            }

            // Si el estado es REVISION, siempre establecer fecha de validación a la actual
            if (inscripcion.IdEstado == 4)
            {
                inscripcion.FechaValidacion = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                var resultado = await _inscripcionService.ActualizarInscripcionAsync(inscripcion, idSesion);

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

            ViewData["Title"] = "Editar Inscripción";
            ViewData["Subtitle"] = "Modificar información de inscripción";
            await CargarCatalogos(idSesion, 111);
            return View(inscripcion);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar Secciones
            var secciones = await _seccionService.ListarSeccionesAsync(idSesion);
            ViewBag.Secciones = secciones ?? new List<Seccion>();

            // Cargar Estudiantes
            // Si es para crear inscripción (110), solo mostrar estudiantes sin inscripciones
            if (idTipoTransaccion == 110)
            {
                var estudiantesSinInscripcion = await _estudianteService.ListarEstudiantesSinInscripcionesAsync(idSesion);
                ViewBag.Estudiantes = estudiantesSinInscripcion ?? new List<Estudiante>();
            }
            else
            {
                // Para editar, mostrar todos los estudiantes (aunque normalmente no se cambia el estudiante)
                var estudiantes = await _estudianteService.ListarEstudiantesAsync(idSesion);
                ViewBag.Estudiantes = estudiantes ?? new List<Estudiante>();
            }

            // Cargar Tipos de Inscripción (Id_Tipo_Catalogo = 18)
            var tiposInscripcion = await _catalogoService.ListarCatalogosPorTipoAsync(18, idSesion);
            ViewBag.TiposInscripcion = tiposInscripcion ?? new List<Catalogo>();

            // Cargar Estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();

            // Cargar Usuarios Validadores: Solo Administrador (1), Coordinador Académico (4), Secretaría Académica (6)
            var validadores = new List<Usuario>();
            
            // Obtener usuarios de cada rol y combinarlos
            var adminUsers = await _usuarioService.FiltrarUsuariosPorRolAsync(1, idSesion); // Administrador
            var coordUsers = await _usuarioService.FiltrarUsuariosPorRolAsync(4, idSesion); // Coordinador Académico
            var secretUsers = await _usuarioService.FiltrarUsuariosPorRolAsync(6, idSesion); // Secretaría Académica
            
            // Combinar y eliminar duplicados por IdUsuario
            var allValidadores = adminUsers.Concat(coordUsers).Concat(secretUsers)
                .GroupBy(u => u.IdUsuario)
                .Select(g => g.First())
                .ToList();
            
            ViewBag.Validadores = allValidadores;
        }
    }
}

