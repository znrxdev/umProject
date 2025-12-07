using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class MateriasController : Controller
    {
        private readonly IMateriaService _materiaService;

        public MateriasController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Materias";
            ViewData["Subtitle"] = "Administración de materias académicas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var materias = await _materiaService.ListarMateriasAsync(idSesion.Value);
                return View(materias ?? new List<Materia>());
            }
            catch (Exception ex)
            {
                // Captura errores controlados de la base de datos (o_Num = -1)
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<Materia>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Materia";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var materias = await _materiaService.FiltrarMateriaPorIdAsync(id, idSesion);
            if (materias == null || materias.Count == 0)
            {
                return NotFound();
            }
            return View(materias.First());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Nueva Materia";
            ViewData["Subtitle"] = "Registrar nueva materia académica";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Materia materia)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            if (ModelState.IsValid)
            {
                // Si el checkbox no está marcado, Activo será false
                materia.Activo = materia.Activo ?? false;
                var resultado = await _materiaService.AgregarMateriaAsync(materia, idSesion);
                
                // o_Num = -1 es error, diferente a -1 es OK
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
            
            ViewData["Title"] = "Nueva Materia";
            ViewData["Subtitle"] = "Registrar nueva materia académica";
            return View(materia);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Materia";
            ViewData["Subtitle"] = "Modificar información de materia";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var materias = await _materiaService.FiltrarMateriaPorIdAsync(id, idSesion);
            if (materias == null || materias.Count == 0)
            {
                return NotFound();
            }
            var materia = materias.First();
            // Asegurar que Activo tenga un valor booleano (no nullable) para el checkbox
            materia.Activo = materia.Activo ?? false;
            return View(materia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Materia materia)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            if (id != materia.IdMateria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Si el checkbox no está marcado, Activo será false
                materia.Activo = materia.Activo ?? false;
                var resultado = await _materiaService.ActualizarMateriaAsync(materia, idSesion);
                
                // o_Num = -1 es error, diferente a -1 es OK
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
            
            ViewData["Title"] = "Editar Materia";
            ViewData["Subtitle"] = "Modificar información de materia";
            return View(materia);
        }
    }
}

