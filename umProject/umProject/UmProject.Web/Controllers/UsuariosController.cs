using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using System.Linq;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPersonaService _personaService;
        private readonly IEstadoService _estadoService;
        private readonly IRolService _rolService;
        private readonly IUsuarioRolService _usuarioRolService;

        public UsuariosController(IUsuarioService usuarioService, IPersonaService personaService, IEstadoService estadoService, IRolService rolService, IUsuarioRolService usuarioRolService)
        {
            _usuarioService = usuarioService;
            _personaService = personaService;
            _estadoService = estadoService;
            _rolService = rolService;
            _usuarioRolService = usuarioRolService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Usuarios";
            ViewData["Subtitle"] = "Administración de usuarios del sistema";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Cargar roles para el filtro
            var roles = await _rolService.ListarRolesAsync(idSesion.Value);
            ViewBag.Roles = roles;

            var usuarios = await _usuarioService.ListarUsuariosAsync(idSesion.Value);
            return View(usuarios);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> BuscarUsuarios(string nombreUsuario, int? idRol)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            List<Usuario> usuarios;

            // Si hay filtro de rol, filtrar primero por rol
            if (idRol.HasValue && idRol.Value > 0)
            {
                usuarios = await _usuarioService.FiltrarUsuariosPorRolAsync(idRol.Value, idSesion.Value);
                
                // Si también hay búsqueda por nombre, filtrar en memoria los resultados del rol
                if (!string.IsNullOrWhiteSpace(nombreUsuario))
                {
                    usuarios = usuarios.Where(u => 
                        u.UsuarioNombre != null && 
                        u.UsuarioNombre.Contains(nombreUsuario, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }
            }
            else if (!string.IsNullOrWhiteSpace(nombreUsuario))
            {
                // Solo buscar por nombre de usuario (Id_Tipo_Transaccion = 24)
                usuarios = await _usuarioService.FiltrarUsuarioPorUsuarioAsync(nombreUsuario, idSesion.Value);
            }
            else
            {
                // Si no hay filtros, retornar todos los usuarios
                usuarios = await _usuarioService.ListarUsuariosAsync(idSesion.Value);
            }

            return PartialView("_TablaUsuarios", usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles del Usuario";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuarios = await _usuarioService.FiltrarUsuariosPorIdAsync(id, idSesion.Value);
            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound();
            }

            return View(usuarios.First());
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? idPersona = null, bool personaCreada = false)
        {
            ViewData["Title"] = "Nuevo Usuario";
            ViewData["Subtitle"] = "Registrar nuevo usuario del sistema";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = new Usuario();
            
            // Cargar estados para crear usuario (Id_Tipo_Transaccion = 20)
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(20, idSesion.Value);
            ViewBag.Estados = estados;
            
            // Si se pasa idPersona, cargar datos de la persona
            if (idPersona.HasValue && idPersona.Value > 0)
            {
                usuario.IdPersona = idPersona.Value;
                var personas = await _personaService.FiltrarPersonaPorIdAsync(idPersona.Value, idSesion.Value);
                if (personas != null && personas.Count > 0)
                {
                    ViewBag.Persona = personas.First();
                    ViewBag.PersonaCreada = personaCreada; // Indicar si viene del flujo de creación
                }
            }
            
            return View(usuario);
        }

        [HttpGet]
        public IActionResult ValidarPersona()
        {
            ViewData["Title"] = "Validar Persona";
            ViewData["Subtitle"] = "Ingrese el número de documento";
            return PartialView("_ValidarPersona");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ValidarPersona(string valorDocumento)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            if (string.IsNullOrWhiteSpace(valorDocumento))
            {
                return Json(new { success = false, message = "El número de documento no puede estar vacío" });
            }

            // Buscar persona por documento (Id_Tipo_Transaccion 18)
            var personas = await _personaService.FiltrarPersonaPorDocumentoAsync(valorDocumento, idSesion.Value);

            // Verificar si la persona existe
            // Si no existe, el SP devuelve una lista vacía (no hace SELECT cuando @o_Num = -1)
            // Si existe, devuelve la persona con IdPersona > 0
            if (personas != null && personas.Count > 0 && personas.First().IdPersona.HasValue && personas.First().IdPersona.Value > 0)
            {
                // La persona existe
                var persona = personas.First();
                
                // Verificar si tiene usuario (Id_Tipo_Transaccion 25)
                var usuarios = await _usuarioService.FiltrarUsuariosPorIdPersonaAsync(persona.IdPersona ?? 0, idSesion.Value);

                if (usuarios != null && usuarios.Count > 0)
                {
                    // La persona tiene usuario - mostrar ventana de decisión
                    var usuario = usuarios.First();
                    return Json(new { 
                        success = true, 
                        personaExiste = true, 
                        tieneUsuario = true,
                        mostrarDecision = true,
                        idPersona = persona.IdPersona,
                        idUsuario = usuario.IdUsuario,
                        urlActualizarPersona = Url.Action("Edit", "Personas", new { id = persona.IdPersona }),
                        urlActualizarUsuario = Url.Action("Edit", "Usuarios", new { id = usuario.IdUsuario }),
                        urlPermisosUsuario = Url.Action("GestionarRoles", "Usuarios", new { idUsuario = usuario.IdUsuario })
                    });
                }
                else
                {
                    // La persona existe pero NO tiene usuario - mostrar opción de agregar usuario
                    return Json(new { 
                        success = true, 
                        personaExiste = true, 
                        tieneUsuario = false,
                        mostrarDecision = true,
                        idPersona = persona.IdPersona,
                        urlAgregarUsuario = Url.Action("Create", "Usuarios", new { idPersona = persona.IdPersona })
                    });
                }
            }
            else
            {
                // La persona no existe - preguntar si se desea crear
                return Json(new { 
                    success = true, 
                    personaExiste = false,
                    mostrarConfirmacion = true,
                    valorDocumento = valorDocumento,
                    urlCrearPersona = Url.Action("Create", "Personas", new { valorDocumento = valorDocumento, crearUsuario = true })
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            ViewData["Title"] = "Nuevo Usuario";
            ViewData["Subtitle"] = "Registrar nuevo usuario del sistema";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Validar que la contraseña no esté vacía al crear
            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                ModelState.AddModelError("Contrasena", "La contraseña es obligatoria.");
            }

            if (ModelState.IsValid)
            {
                var resultado = await _usuarioService.AgregarUsuariosAsync(usuario, idSesion.Value);
                if (resultado.Exitoso)
                {
                    // Obtener el IdUsuario creado del resultado
                    var idUsuarioCreado = resultado.Codigo;
                    
                    // Preguntar si quiere agregar roles
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    TempData["IdUsuarioCreado"] = idUsuarioCreado;
                    TempData["AgregarRoles"] = true;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", resultado.Mensaje);
            }

            // Cargar estados para crear usuario (Id_Tipo_Transaccion = 20)
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(20, idSesion.Value);
            ViewBag.Estados = estados;

            // Recargar datos de persona si existe
            if (usuario.IdPersona.HasValue && usuario.IdPersona.Value > 0)
            {
                var personas = await _personaService.FiltrarPersonaPorIdAsync(usuario.IdPersona.Value, idSesion.Value);
                if (personas != null && personas.Count > 0)
                {
                    ViewBag.Persona = personas.First();
                    ViewBag.PersonaCreada = Request.Query.ContainsKey("personaCreada");
                }
            }

            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuarios = await _usuarioService.FiltrarUsuariosPorIdAsync(id, idSesion.Value);
            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound();
            }

            var usuario = usuarios.First();
            
            // Obtener datos de la persona
            var personas = await _personaService.FiltrarPersonaPorIdAsync(usuario.IdPersona ?? 0, idSesion.Value);
            var persona = personas?.FirstOrDefault();
            
            // Si es una petición AJAX, retornar JSON con las opciones de edición
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || 
                Request.Headers["Accept"].ToString().Contains("application/json"))
            {
                return Json(new
                {
                    success = true,
                    mostrarDecisionEdicion = true,
                    idUsuario = usuario.IdUsuario,
                    idPersona = usuario.IdPersona,
                    urlEditarUsuario = Url.Action("EditarUsuario", "Usuarios", new { id = usuario.IdUsuario }),
                    urlEditarPersona = persona != null ? Url.Action("Edit", "Personas", new { id = persona.IdPersona }) : null,
                    urlGestionarRoles = Url.Action("GestionarRoles", "Usuarios", new { idUsuario = usuario.IdUsuario })
                });
            }
            
            // Si no es AJAX, redirigir a EditarUsuario (vista normal de edición)
            return RedirectToAction("EditarUsuario", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(int id)
        {
            ViewData["Title"] = "Editar Usuario";
            ViewData["Subtitle"] = "Modificar información de usuario";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Cargar estados para editar usuario (Id_Tipo_Transaccion = 21)
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(21, idSesion.Value);
            ViewBag.Estados = estados;

            var usuarios = await _usuarioService.FiltrarUsuariosPorIdAsync(id, idSesion.Value);
            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound();
            }

            return View("Edit", usuarios.First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            ViewData["Title"] = "Editar Usuario";
            ViewData["Subtitle"] = "Modificar información de usuario";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            // Si la contraseña está vacía o solo tiene espacios, establecerla como null
            // Esto permite no actualizar la contraseña si el usuario no la cambia
            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                usuario.Contrasena = null;
                // Remover el error de validación de contraseña si existe
                ModelState.Remove("Contrasena");
            }

            if (ModelState.IsValid)
            {
                var resultado = await _usuarioService.ActualizarUsuariosAsync(usuario, idSesion.Value);
                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", resultado.Mensaje);
            }

            // Cargar estados para editar usuario (Id_Tipo_Transaccion = 21)
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(21, idSesion.Value);
            ViewBag.Estados = estados;

            // Retornar la vista Edit explícitamente
            return View("Edit", usuario);
        }

        [HttpGet]
        public async Task<IActionResult> GestionarRoles(int idUsuario)
        {
            ViewData["Title"] = "Gestión de Roles";
            ViewData["Subtitle"] = "Administrar roles del usuario";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Obtener datos del usuario
            var usuarios = await _usuarioService.FiltrarUsuariosPorIdAsync(idUsuario, idSesion.Value);
            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound();
            }

            var usuario = usuarios.First();

            // Cargar roles disponibles
            var roles = await _rolService.ListarRolesAsync(idSesion.Value);
            ViewBag.Roles = roles;

            // Cargar roles del usuario
            var rolesUsuario = await _usuarioRolService.ListarRolesPorUsuarioAsync(idUsuario, idSesion.Value);
            ViewBag.RolesUsuario = rolesUsuario;

            ViewBag.IdUsuario = idUsuario;

            // Pasar el usuario como modelo
            return View(usuario);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GuardarUsuarioRol(int idUsuario, int idRol, int? idUsuarioRol, bool activo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var usuarioRol = new UsuarioRol
            {
                IdUsuario = idUsuario,
                IdRol = idRol,
                IdUsuarioRol = idUsuarioRol ?? 0,
                Activo = activo
            };

            ResultadoOperacion resultado;
            if (idUsuarioRol.HasValue && idUsuarioRol.Value > 0)
            {
                // Actualizar
                resultado = await _usuarioRolService.ActualizarUsuarioRolAsync(usuarioRol, idSesion.Value);
            }
            else
            {
                // Agregar
                resultado = await _usuarioRolService.AgregarUsuarioRolAsync(usuarioRol, idSesion.Value);
            }

            return Json(new { success = resultado.Exitoso, message = resultado.Mensaje });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ObtenerRolesUsuario(int idUsuario)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            var rolesUsuario = await _usuarioRolService.ListarRolesPorUsuarioAsync(idUsuario, idSesion.Value);
            var roles = await _rolService.ListarRolesAsync(idSesion.Value);

            // Combinar datos para mostrar nombre del rol
            var rolesConNombre = rolesUsuario.Select(ur => new
            {
                IdUsuarioRol = ur.IdUsuarioRol,
                IdUsuario = ur.IdUsuario,
                IdRol = ur.IdRol,
                NombreRol = roles.FirstOrDefault(r => r.IdRol == ur.IdRol)?.NombreRol ?? "N/A",
                Activo = ur.Activo,
                FechaCreacion = ur.FechaCreacion
            }).ToList();

            return Json(new { success = true, roles = rolesConNombre });
        }
    }
}

