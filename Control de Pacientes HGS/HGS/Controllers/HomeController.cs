using HGS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class HomeController : Controller
    {
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
        public async Task<IActionResult> Login(string username, string password, string checkbox)
        {
            if (checkbox != null)
            {
                if (username.Equals("ADMINISTRADOR_HGS") && password.Equals("#HGS_20234dMin"))
                {
                    return View("Index");
                }
                else
                {
                    @ViewData["Response"] = "IncorrectAdmin";
                    return View();
                }
            }

            //Validar en BD si el Dr. existe
            HGSModel.Doctor aDoctor = new()
            {
                CollegiateNumber = "100000000",
                User = username,
                Password = password,
                ConfirmedPassword = password,
                Dpi = "1000000000000",
                Name = "ABC",
                Lastname = "ABC"
            };

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult?>.DoctorExists(aDoctor);

            if (generalResult != null)
            {
                if (generalResult.Message != null)
                {
                    if (generalResult.Message.Equals("Correct"))
                    {
                        return View("Index");
                    }
                    @ViewData["Response"] = generalResult.Message;
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }        
    }
}