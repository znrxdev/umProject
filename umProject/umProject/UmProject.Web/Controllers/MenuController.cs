using Microsoft.AspNetCore.Mvc;
using UmProject.Web.Filters;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class MenuController : Controller
    {
        public IActionResult MenuNoImplementado(string nombreMenu)
        {
            ViewData["Title"] = nombreMenu ?? "Menú";
            ViewData["Subtitle"] = "Funcionalidad en desarrollo";
            ViewBag.NombreMenu = nombreMenu ?? "Este menú";
            return View();
        }
    }
}

