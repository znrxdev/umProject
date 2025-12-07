using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using UmProject.Web.Models;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class ProgramasBecasController : Controller
    {
        private readonly IBecaProgramaService _becaProgramaService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEstadoService _estadoService;
        private readonly IBecaCriterioService _becaCriterioService;

        public ProgramasBecasController(
            IBecaProgramaService becaProgramaService,
            ICatalogoService catalogoService,
            IEstadoService estadoService,
            IBecaCriterioService becaCriterioService)
        {
            _becaProgramaService = becaProgramaService;
            _catalogoService = catalogoService;
            _estadoService = estadoService;
            _becaCriterioService = becaCriterioService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Programas de Becas";
            ViewData["Subtitle"] = "Administración de programas de becas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var programas = await _becaProgramaService.ListarBecaProgramasAsync(idSesion.Value);
                return View(programas ?? new List<BecaPrograma>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<BecaPrograma>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Programa de Beca";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var programa = await _becaProgramaService.ObtenerBecaProgramaPorIdAsync(id, idSesion);
            if (programa == null)
            {
                TempData["ErrorMessage"] = "Programa de beca no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(programa);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nuevo Programa de Beca";
            ViewData["Subtitle"] = "Registrar nuevo programa de beca";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 59); // 59 = AGREGAR PROGRAMA DE BECA

            var modelo = new BecaPrograma
            {
                IdEstadoPrograma = 4, // EN REVISION por defecto
                CodigoPrograma = GenerarCodigoPrograma(),
                RequiereSinSanciones = true
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BecaPrograma becaPrograma)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(becaPrograma.CodigoPrograma))
                {
                    becaPrograma.CodigoPrograma = GenerarCodigoPrograma();
                }

                becaPrograma.IdEstadoPrograma ??= 4; // EN REVISION

                var resultado = await _becaProgramaService.AgregarBecaProgramaAsync(becaPrograma, idSesion);

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

            ViewData["Title"] = "Nuevo Programa de Beca";
            ViewData["Subtitle"] = "Registrar nuevo programa de beca";
            await CargarCatalogos(idSesion, 59);
            return View(becaPrograma);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Programa de Beca";
            ViewData["Subtitle"] = "Modificar información de programa de beca";

            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var programa = await _becaProgramaService.ObtenerBecaProgramaPorIdAsync(id, idSesion);
            if (programa == null)
            {
                TempData["ErrorMessage"] = "Programa de beca no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCatalogos(idSesion, 60); // 60 = ACTUALIZAR PROGRAMA DE BECA
            return View(programa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BecaPrograma becaPrograma)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            if (id != becaPrograma.IdBecaPrograma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _becaProgramaService.ActualizarBecaProgramaAsync(becaPrograma, idSesion);

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

            ViewData["Title"] = "Editar Programa de Beca";
            ViewData["Subtitle"] = "Modificar información de programa de beca";
            await CargarCatalogos(idSesion, 60);
            return View(becaPrograma);
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // TODO: Ajustar los IDs de tipo de catálogo según la base de datos
            // Cargar Tipos de Programa (ajustar el ID según la base de datos)
            var tiposPrograma = await _catalogoService.ListarCatalogosPorTipoAsync(20, idSesion); // ID 20 es ejemplo, ajustar según BD
            ViewBag.TiposPrograma = tiposPrograma ?? new List<Catalogo>();

            // Cargar Modalidades de Programa (ajustar el ID según la base de datos)
            var modalidadesPrograma = await _catalogoService.ListarCatalogosPorTipoAsync(21, idSesion); // ID 21 es ejemplo, ajustar según BD
            ViewBag.ModalidadesPrograma = modalidadesPrograma ?? new List<Catalogo>();

            // Cargar Monedas (ajustar el ID según la base de datos)
            var monedas = await _catalogoService.ListarCatalogosPorTipoAsync(22, idSesion); // ID 22 es ejemplo, ajustar según BD
            ViewBag.Monedas = monedas ?? new List<Catalogo>();

            // Cargar Estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();
        }

        [HttpGet]
        public async Task<IActionResult> Criterios(int programaId, int? criterioId = null)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var programa = await _becaProgramaService.ObtenerBecaProgramaPorIdAsync(programaId, idSesion);
            if (programa == null)
            {
                TempData["ErrorMessage"] = "Programa de beca no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var criterios = await _becaCriterioService.ListarPorProgramaAsync(programaId, idSesion);
            var formCriterio = criterioId.HasValue
                ? await _becaCriterioService.ObtenerPorIdAsync(criterioId.Value, idSesion) ?? new BecaCriterio()
                : new BecaCriterio
                {
                    IdPrograma = programaId,
                    Codigo = GenerarCodigoCriterio(programa.CodigoPrograma),
                    RequiereSoporte = false,
                    Activo = true
                };

            await CargarCatalogosCriterios(idSesion);

            var vm = new BecaProgramaCriteriosViewModel
            {
                Programa = programa,
                Criterios = criterios,
                FormCriterio = formCriterio
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarCriterio([Bind(Prefix = "FormCriterio")] BecaCriterio criterio)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            if (!ModelState.IsValid)
            {
                return await CargarVistaCriteriosConErrores(criterio.IdPrograma ?? 0, criterio);
            }

            ResultadoOperacion resultado;
            if (criterio.IdBecaCriterio == null)
            {
                if (string.IsNullOrWhiteSpace(criterio.Codigo))
                {
                    criterio.Codigo = GenerarCodigoCriterio("BP");
                }

                resultado = await _becaCriterioService.AgregarAsync(criterio, idSesion);
            }
            else
            {
                resultado = await _becaCriterioService.ActualizarAsync(criterio, idSesion);
            }

            if (resultado.Codigo == -1)
            {
                TempData["ErrorMessage"] = resultado.Mensaje;
                return await CargarVistaCriteriosConErrores(criterio.IdPrograma ?? 0, criterio);
            }

            TempData["SuccessMessage"] = resultado.Mensaje;
            return RedirectToAction(nameof(Criterios), new { programaId = criterio.IdPrograma });
        }

        private async Task<IActionResult> CargarVistaCriteriosConErrores(int programaId, BecaCriterio criterio)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var programa = await _becaProgramaService.ObtenerBecaProgramaPorIdAsync(programaId, idSesion);
            var criterios = await _becaCriterioService.ListarPorProgramaAsync(programaId, idSesion);
            await CargarCatalogosCriterios(idSesion);

            var vm = new BecaProgramaCriteriosViewModel
            {
                Programa = programa,
                Criterios = criterios,
                FormCriterio = criterio
            };

            return View("Criterios", vm);
        }

        private async Task CargarCatalogosCriterios(int idSesion)
        {
            var tiposCriterio = await _catalogoService.ListarCatalogosPorTipoAsync(13, idSesion); // TIPO CRITERIO
            ViewBag.TiposCriterio = tiposCriterio ?? new List<Catalogo>();

            ViewBag.TiposDato = new List<string> { "NUMERICO", "TEXTO", "BOOLEANO" };
            ViewBag.Operadores = new List<string> { "=", "<>", ">", ">=", "<", "<=", "BETWEEN" };
        }

        private static string GenerarCodigoPrograma()
        {
            var sufijo = Guid.NewGuid().ToString("N")[..4].ToUpperInvariant();
            return $"BP-{DateTime.UtcNow:yyyyMMddHHmmss}-{sufijo}";
        }

        private static string GenerarCodigoCriterio(string? codigoPrograma)
        {
            var sufijo = Guid.NewGuid().ToString("N")[..4].ToUpperInvariant();
            return $"{codigoPrograma ?? "BP"}-CR-{DateTime.UtcNow:yyMMddHHmm}-{sufijo}";
        }
    }
}

