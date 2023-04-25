using HGS.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HGS.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
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
                    // Seguridad
                    var claims = new List<Claim>
                    {
                        new Claim("username", username),
                        new Claim(ClaimTypes.NameIdentifier, "1234")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    //

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

            HGSModel.Token? token = await APIService<HGSModel.Token>.LoginAPILogin(
                new HGSModel.Token
                {
                    _token = "AUF){whU8:nUvg6=ce4k5y=qGed(#&"
                });

            if (token != null)
            {
                if (string.IsNullOrEmpty(token._token))
                {
                    return NotFound();
                }
            }

            HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult?>.DoctorExists(aDoctor, token._token);

            if (generalResult != null)
            {
                if (generalResult.Message != null)
                {
                    if (generalResult.Message.Equals("Correct"))
                    {
                        int id = generalResult.Id;

                        // Seguridad
                        var claims = new List<Claim>
                        {
                            new Claim("username", username),
                            new Claim(ClaimTypes.NameIdentifier, "1234")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        //

                        return RedirectToAction("Index", "Appointment", new { id });
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

        [HttpPost]
        public async Task<JsonResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Json(new { success = true });
        }
    }
}