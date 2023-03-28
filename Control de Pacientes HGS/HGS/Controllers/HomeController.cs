using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
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
                    @ViewData["Response"] = "Incorrect";
                    return View();
                }
            }

            //Validar en BD si el Dr. existe
            return View("Index");
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}