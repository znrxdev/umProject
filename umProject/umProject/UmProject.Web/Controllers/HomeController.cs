using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class HomeController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public HomeController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Obtener menús del usuario
            var menusJson = HttpContext.Session.GetString("Menus");
            List<Menu> menus = new List<Menu>();
            
            if (!string.IsNullOrEmpty(menusJson))
            {
                menus = JsonSerializer.Deserialize<List<Menu>>(menusJson) ?? new List<Menu>();
            }
            else
            {
                menus = await _usuarioService.ListarMenuPorRolAsync(idSesion.Value);
                HttpContext.Session.SetString("Menus", JsonSerializer.Serialize(menus));
            }

            ViewBag.Menus = menus;
            ViewBag.UsuarioSesion = HttpContext.Session.GetString("UsuarioSesion");
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
