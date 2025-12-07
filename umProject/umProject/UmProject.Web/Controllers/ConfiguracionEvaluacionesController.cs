using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Data;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class ConfiguracionEvaluacionesController : Controller
    {
        private readonly IEvaluacionInstanciaService _evaluacionInstanciaService;
        private readonly ISeccionService _seccionService;
        private readonly IPeriodoAcademicoService _periodoAcademicoService;
        private readonly IEstadoService _estadoService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEvaluacionModeloService _evaluacionModeloService;
        private readonly IRolService _rolService;
        private readonly IConexionService _conexionService;
        private readonly IMateriaService _materiaService;

        public ConfiguracionEvaluacionesController(
            IEvaluacionInstanciaService evaluacionInstanciaService,
            ISeccionService seccionService,
            IPeriodoAcademicoService periodoAcademicoService,
            IEstadoService estadoService,
            ICatalogoService catalogoService,
            IEvaluacionModeloService evaluacionModeloService,
            IRolService rolService,
            IConexionService conexionService,
            IMateriaService materiaService)
        {
            _evaluacionInstanciaService = evaluacionInstanciaService;
            _seccionService = seccionService;
            _periodoAcademicoService = periodoAcademicoService;
            _estadoService = estadoService;
            _catalogoService = catalogoService;
            _evaluacionModeloService = evaluacionModeloService;
            _rolService = rolService;
            _conexionService = conexionService;
            _materiaService = materiaService;
        }

        public async Task<IActionResult> Index(string tab = "instancias")
        {
            ViewData["Title"] = "Configuración de Evaluaciones";
            ViewData["Subtitle"] = "Gestión de instancias y modelos de evaluación";
            ViewData["ActiveTab"] = tab;
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var instancias = await _evaluacionInstanciaService.ListarEvaluacionesInstanciasAsync(idSesion.Value);
                var modelos = await _evaluacionModeloService.ListarEvaluacionesModelosAsync(idSesion.Value);
                
                ViewBag.Instancias = instancias ?? new List<EvaluacionInstancia>();
                ViewBag.Modelos = modelos ?? new List<EvaluacionModelo>();
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.Instancias = new List<EvaluacionInstancia>();
                ViewBag.Modelos = new List<EvaluacionModelo>();
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Instancia de Evaluación";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var instancia = await _evaluacionInstanciaService.ObtenerEvaluacionInstanciaPorIdAsync(id, idSesion);
            if (instancia == null)
            {
                TempData["ErrorMessage"] = "Instancia de evaluación no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(instancia);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nueva Instancia de Evaluación";
            ViewData["Subtitle"] = "Crear nueva instancia de evaluación";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogosCreate(idSesion, 124); // 124 = AGREGAR EVALUACIÓN INSTANCIA
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EvaluacionInstancia evaluacionInstancia)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            // Establecer estado EN REVISION automáticamente al crear
            evaluacionInstancia.IdEstado = 4; // EN REVISION

            // Validar ModelState y mostrar errores si hay
            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}")
                    .ToList();
                
                var mensajeErrores = string.Join(" | ", errores);
                TempData["ErrorMessage"] = $"Errores de validación: {mensajeErrores}";
                
                ViewData["Title"] = "Nueva Instancia de Evaluación";
                ViewData["Subtitle"] = "Crear nueva instancia de evaluación";
                await CargarCatalogosCreate(idSesion, 124);
                return View(evaluacionInstancia);
            }

            var resultado = await _evaluacionInstanciaService.AgregarEvaluacionInstanciaAsync(evaluacionInstancia, idSesion);

            if (resultado.Exitoso)
            {
                TempData["SuccessMessage"] = string.IsNullOrWhiteSpace(resultado.Mensaje)
                    ? "Instancia de evaluación creada correctamente."
                    : resultado.Mensaje;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = string.IsNullOrWhiteSpace(resultado.Mensaje)
                    ? "La instancia no se pudo crear. Revise los datos y permisos."
                    : resultado.Mensaje;
            }

            ViewData["Title"] = "Nueva Instancia de Evaluación";
            ViewData["Subtitle"] = "Crear nueva instancia de evaluación";
            await CargarCatalogosCreate(idSesion, 124);
            return View(evaluacionInstancia);
        }

        [HttpGet]
        public async Task<IActionResult> GetSeccionesPorPeriodo(int idPeriodo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var secciones = await _seccionService.ListarSeccionesAsync(idSesion, idPeriodo);
            return Json(secciones ?? new List<Seccion>());
        }

        [HttpGet]
        public async Task<IActionResult> GetModelosPorSeccion(int idSeccion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            // Obtener la sección para obtener el Id_Materia_Periodo
            var seccion = await _seccionService.ObtenerSeccionPorIdAsync(idSeccion, idSesion);
            if (seccion == null || !seccion.IdMateriaPeriodo.HasValue)
            {
                return Json(new List<EvaluacionModelo>());
            }
            
            // Obtener Id_Materia desde la materia-período
            int? idMateria = null;
            using (var conexion = _conexionService.ObtenerConexion())
            {
                using var cmd = new Microsoft.Data.SqlClient.SqlCommand("usp_materias_periodos", conexion)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@Id_Tipo_Transaccion", System.Data.SqlDbType.Int).Value = 93; // FILTRAR POR ID MATERIA PERIODO
                cmd.Parameters.Add("@Id_Sesion", System.Data.SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Materia_Periodo", System.Data.SqlDbType.Int).Value = seccion.IdMateriaPeriodo.Value;
                cmd.Parameters.Add("@o_Num", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", System.Data.SqlDbType.NVarChar, 255).Direction = System.Data.ParameterDirection.Output;
                
                await conexion.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        idMateria = reader["Id_Materia"] as int?;
                    }
                }
            }
            
            if (!idMateria.HasValue)
            {
                return Json(new List<EvaluacionModelo>());
            }
            
            // Obtener modelos de evaluación por materia usando el repository directamente
            var repository = new UmProject.Data.EvaluacionModeloRepository(_conexionService);
            var resultado = await repository.FiltrarEvaluacionModeloPorMateriaAsync(idMateria.Value, idSesion);
            var modelosFiltrados = resultado?.Datos ?? new List<EvaluacionModelo>();
            
            return Json(modelosFiltrados);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Instancia de Evaluación";
            ViewData["Subtitle"] = "Modificar información de instancia";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var instancia = await _evaluacionInstanciaService.ObtenerEvaluacionInstanciaPorIdAsync(id, idSesion);
            if (instancia == null)
            {
                TempData["ErrorMessage"] = "Instancia de evaluación no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCatalogos(idSesion, 125); // 125 = ACTUALIZAR EVALUACIÓN INSTANCIA
            return View(instancia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EvaluacionInstancia evaluacionInstancia)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != evaluacionInstancia.IdEvaluacionInstancia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _evaluacionInstanciaService.ActualizarEvaluacionInstanciaAsync(evaluacionInstancia, idSesion);

                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Editar Instancia de Evaluación";
            ViewData["Subtitle"] = "Modificar información de instancia";
            await CargarCatalogos(idSesion, 125);
            return View(evaluacionInstancia);
        }

        // ========== ACCIONES PARA MODELOS DE EVALUACIÓN ==========
        
        [HttpGet]
        public async Task<IActionResult> ModelosEvaluacionDetails(int id)
        {
            ViewData["Title"] = "Detalles de Modelo de Evaluación";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var modelo = await _evaluacionModeloService.ObtenerEvaluacionModeloPorIdAsync(id, idSesion);
            if (modelo == null)
            {
                TempData["ErrorMessage"] = "Modelo de evaluación no encontrado.";
                return RedirectToAction(nameof(Index), new { tab = "modelos" });
            }
            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ModelosEvaluacionCreate()
        {
            ViewData["Title"] = "Nuevo Modelo de Evaluación";
            ViewData["Subtitle"] = "Crear nuevo modelo de evaluación";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogosModelos(idSesion, 120); // 120 = AGREGAR EVALUACIÓN MODELO
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModelosEvaluacionCreate(EvaluacionModelo evaluacionModelo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (ModelState.IsValid)
            {
                var resultado = await _evaluacionModeloService.AgregarEvaluacionModeloAsync(evaluacionModelo, idSesion);

                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index), new { tab = "modelos" });
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Nuevo Modelo de Evaluación";
            ViewData["Subtitle"] = "Crear nuevo modelo de evaluación";
            await CargarCatalogosModelos(idSesion, 120);
            return View(evaluacionModelo);
        }

        [HttpGet]
        public async Task<IActionResult> ModelosEvaluacionEdit(int id)
        {
            ViewData["Title"] = "Editar Modelo de Evaluación";
            ViewData["Subtitle"] = "Modificar información de modelo";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var modelo = await _evaluacionModeloService.ObtenerEvaluacionModeloPorIdAsync(id, idSesion);
            if (modelo == null)
            {
                TempData["ErrorMessage"] = "Modelo de evaluación no encontrado.";
                return RedirectToAction(nameof(Index), new { tab = "modelos" });
            }

            await CargarCatalogosModelos(idSesion, 121); // 121 = ACTUALIZAR EVALUACIÓN MODELO
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModelosEvaluacionEdit(int id, EvaluacionModelo evaluacionModelo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != evaluacionModelo.IdEvaluacionModelo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _evaluacionModeloService.ActualizarEvaluacionModeloAsync(evaluacionModelo, idSesion);

                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index), new { tab = "modelos" });
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Editar Modelo de Evaluación";
            ViewData["Subtitle"] = "Modificar información de modelo";
            await CargarCatalogosModelos(idSesion, 121);
            return View(evaluacionModelo);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar Secciones
            var secciones = await _seccionService.ListarSeccionesAsync(idSesion);
            ViewBag.Secciones = secciones ?? new List<Seccion>();

            // Cargar Períodos Académicos
            var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
            ViewBag.Periodos = periodos ?? new List<PeriodoAcademico>();

            // Cargar Estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();

            // Cargar Modelos de Evaluación
            var modelosEvaluacion = await _evaluacionModeloService.ListarEvaluacionesModelosAsync(idSesion);
            ViewBag.ModelosEvaluacion = modelosEvaluacion ?? new List<EvaluacionModelo>();
        }

        private async Task CargarCatalogosCreate(int idSesion, int idTipoTransaccion)
        {
            // Cargar solo Períodos Académicos EN REVISION (Id_Estado = 4)
            var todosLosPeriodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
            var periodosEnRevision = todosLosPeriodos?.Where(p => p.IdEstado == 4).ToList() ?? new List<PeriodoAcademico>();
            ViewBag.Periodos = periodosEnRevision;

            // No cargar secciones ni modelos inicialmente, se cargarán vía AJAX
            ViewBag.Secciones = new List<Seccion>();
            ViewBag.ModelosEvaluacion = new List<EvaluacionModelo>();

            // Estado fijo: EN REVISION (4)
            ViewBag.IdEstadoFijo = 4;
            ViewBag.NombreEstadoFijo = "EN REVISION";
        }

        private async Task CargarCatalogosModelos(int idSesion, int idTipoTransaccion)
        {
            // Cargar tipos de evaluación (Tipo Catálogo = 8 según umDbData.sql)
            var tiposEvaluacion = await _catalogoService.ListarCatalogosPorTipoAsync(8, idSesion);
            ViewBag.TiposEvaluacion = tiposEvaluacion ?? new List<Catalogo>();

            // Cargar materias
            var materias = await _materiaService.ListarMateriasAsync(idSesion);
            ViewBag.Materias = materias ?? new List<Materia>();
        }
    }
}

