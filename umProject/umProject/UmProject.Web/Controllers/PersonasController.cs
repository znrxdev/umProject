using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class PersonasController : Controller
    {
        private readonly IPersonaService _personaService;
        private readonly ICatalogoService _catalogoService;

        public PersonasController(IPersonaService personaService, ICatalogoService catalogoService)
        {
            _personaService = personaService;
            _catalogoService = catalogoService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Personas";
            ViewData["Subtitle"] = "Administración de personas";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var personas = await _personaService.ListarPersonasAsync(idSesion);
            return View(personas);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Persona";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var personas = await _personaService.FiltrarPersonaPorIdAsync(id, idSesion);
            if (personas == null || personas.Count == 0)
            {
                return NotFound();
            }
            return View(personas.First());
        }

        [HttpGet]
        public async Task<IActionResult> Create(string? valorDocumento = null, bool crearUsuario = false)
        {
            ViewData["Title"] = "Nueva Persona";
            ViewData["Subtitle"] = "Registrar nueva persona";
            ViewBag.CrearUsuario = crearUsuario;
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion);
            
            var persona = new Persona();
            if (!string.IsNullOrEmpty(valorDocumento))
            {
                persona.ValorDocumento = valorDocumento;
            }
            
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona, bool crearUsuario = false)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            // Obtener crearUsuario del formulario si no viene como parámetro
            if (!crearUsuario)
            {
                var crearUsuarioValue = Request.Form["crearUsuario"].ToString();
                crearUsuario = crearUsuarioValue == "true" || crearUsuarioValue == "True";
            }
            
            if (ModelState.IsValid)
            {
                persona.IdEstado = 1; // Activo
                var resultado = await _personaService.AgregarPersonaAsync(persona, idSesion);
                
                // El stored procedure devuelve SCOPE_IDENTITY() en @o_Num cuando es exitoso
                // Si resultado.Codigo > 0, contiene el Id_Persona creado
                if (resultado.Codigo > 0)
                {
                    if (crearUsuario)
                    {
                        // Si viene del flujo de crear usuario, redirigir a crear usuario con el IdPersona
                        TempData["SuccessMessage"] = resultado.Mensaje + " Ahora puede crear el usuario.";
                        return RedirectToAction("Create", "Usuarios", new { idPersona = resultado.Codigo, personaCreada = true });
                    }
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", resultado.Mensaje);
            }
            
            await CargarCatalogos(idSesion);
            ViewBag.CrearUsuario = crearUsuario;
            ViewData["Title"] = "Nueva Persona";
            ViewData["Subtitle"] = "Registrar nueva persona";
            return View(persona);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Persona";
            ViewData["Subtitle"] = "Modificar información de persona";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var personas = await _personaService.FiltrarPersonaPorIdAsync(id, idSesion);
            if (personas == null || personas.Count == 0)
            {
                return NotFound();
            }
            
            await CargarCatalogos(idSesion);
            return View(personas.First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            if (id != persona.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var resultado = await _personaService.ActualizarPersonaAsync(persona, idSesion);
                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", resultado.Mensaje);
            }
            
            await CargarCatalogos(idSesion);
            ViewData["Title"] = "Editar Persona";
            ViewData["Subtitle"] = "Modificar información de persona";
            return View(persona);
        }

        private async Task CargarCatalogos(int idSesion)
        {
            // Tipos de documento (ID 1), Géneros (ID 2), Nacionalidades (ID 3), Estados civiles (ID 4)
            // Ajustar según los IDs reales en tu base de datos
            ViewBag.TiposDocumento = await _catalogoService.ListarCatalogosPorTipoAsync(1, idSesion);
            ViewBag.Generos = await _catalogoService.ListarCatalogosPorTipoAsync(2, idSesion);
            ViewBag.Nacionalidades = await _catalogoService.ListarCatalogosPorTipoAsync(3, idSesion);
            ViewBag.EstadosCiviles = await _catalogoService.ListarCatalogosPorTipoAsync(4, idSesion);
        }
    }
}

