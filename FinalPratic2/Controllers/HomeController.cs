using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

namespace FinalPratic2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
