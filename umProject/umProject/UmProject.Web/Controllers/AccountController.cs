using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Models;

namespace UmProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AccountController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("IdSesion") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuarios = await _usuarioService.InicioSesionAsync(model.Usuario, model.Contrasena);

            if (usuarios != null && usuarios.Count > 0)
            {
                var usuario = usuarios.First();
                
                // Actualizar última sesión después de validar contraseña correctamente
                if (usuario.IdUsuario.HasValue && usuario.IdUsuario.Value > 0)
                {
                    await _usuarioService.ActualizarUltimaSesionAsync(usuario.IdUsuario.Value);
                }
                
                // Guardar sesión
                HttpContext.Session.SetInt32("IdSesion", usuario.IdUsuario ?? 0);
                HttpContext.Session.SetInt32("IdPersonaSesion", usuario.IdPersona ?? 0);
                HttpContext.Session.SetString("UsuarioSesion", usuario.UsuarioNombre ?? string.Empty);

                // Obtener menús del usuario
                var menus = await _usuarioService.ListarMenuPorRolAsync(usuario.IdUsuario ?? 0);
                HttpContext.Session.SetString("Menus", System.Text.Json.JsonSerializer.Serialize(menus));

                return RedirectToAction("Index", "Home");
            }

            model.MensajeError = "Usuario o contraseña incorrectos";
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

