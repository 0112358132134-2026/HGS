using HGS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HGS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string checkbox)
        {
            if (checkbox != null)
            {
                if (username.Equals("ADMINISTRADOR_HGS") && password.Equals("#HGS_20234dMin"))
                {
                    return View("Index");
                }
                else
                {
                    @ViewData["Resultado"] = "Nombre o contraseña incorrecta";
                    return View();
                }
            }

            //Validar en BD
            return View("Index");
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}