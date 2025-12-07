using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class ModelosEvaluacionController : Controller
    {
        private readonly IEvaluacionModeloService _evaluacionModeloService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEstadoService _estadoService;
        private readonly IRolService _rolService;
        private readonly IMateriaService _materiaService;

        public ModelosEvaluacionController(
            IEvaluacionModeloService evaluacionModeloService,
            ICatalogoService catalogoService,
            IEstadoService estadoService,
            IRolService rolService,
            IMateriaService materiaService)
        {
            _evaluacionModeloService = evaluacionModeloService;
            _catalogoService = catalogoService;
            _estadoService = estadoService;
            _rolService = rolService;
            _materiaService = materiaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Modelos de Evaluación";
            ViewData["Subtitle"] = "Gestión de modelos de evaluación";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var modelos = await _evaluacionModeloService.ListarEvaluacionesModelosAsync(idSesion.Value);
                return View(modelos ?? new List<EvaluacionModelo>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<EvaluacionModelo>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Modelo de Evaluación";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var modelo = await _evaluacionModeloService.ObtenerEvaluacionModeloPorIdAsync(id, idSesion);
            if (modelo == null)
            {
                TempData["ErrorMessage"] = "Modelo de evaluación no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nuevo Modelo de Evaluación";
            ViewData["Subtitle"] = "Crear nuevo modelo de evaluación";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 120); // 120 = AGREGAR EVALUACIÓN MODELO
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EvaluacionModelo evaluacionModelo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (ModelState.IsValid)
            {
                var resultado = await _evaluacionModeloService.AgregarEvaluacionModeloAsync(evaluacionModelo, idSesion);

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

            ViewData["Title"] = "Nuevo Modelo de Evaluación";
            ViewData["Subtitle"] = "Crear nuevo modelo de evaluación";
            await CargarCatalogos(idSesion, 120);
            return View(evaluacionModelo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Modelo de Evaluación";
            ViewData["Subtitle"] = "Modificar información de modelo";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var modelo = await _evaluacionModeloService.ObtenerEvaluacionModeloPorIdAsync(id, idSesion);
            if (modelo == null)
            {
                TempData["ErrorMessage"] = "Modelo de evaluación no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCatalogos(idSesion, 121); // 121 = ACTUALIZAR EVALUACIÓN MODELO
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EvaluacionModelo evaluacionModelo)
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
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }

            ViewData["Title"] = "Editar Modelo de Evaluación";
            ViewData["Subtitle"] = "Modificar información de modelo";
            await CargarCatalogos(idSesion, 121);
            return View(evaluacionModelo);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
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

