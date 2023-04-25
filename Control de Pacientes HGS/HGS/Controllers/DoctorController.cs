using HGS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HGS.Controllers
{
    public class DoctorController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
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

            IEnumerable<HGSModel.Doctor>? doctors = await APIService<HGSModel.Doctor>.GetList("Doctor/GetList", token._token);
            return View(doctors);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
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

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.SpecialGet("Doctor/GetS", token._token);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CollegiateNumber", "User", "Password", "ConfirmedPassword", "Dpi", "Name", "Lastname", "Birthdate", "SpecialtyId")] HGSModel.Doctor newDoctor)
        {
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

            if (!newDoctor.Password.Equals(newDoctor.ConfirmedPassword))
            {
                @ViewData["Response"] = "InconsistentPassword";
            }
            else
            {                
                // Creamos el User del Doctor                
                newDoctor.User = GenerateUserName(newDoctor.Name, newDoctor.Lastname);
                HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Set(newDoctor, "Doctor/Set", token._token);

                if (generalResult != null)
                {
                    @ViewData["Response"] = generalResult.Message;
                }                
            }

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.SpecialGet("Doctor/GetS", token._token);
            return View(doctor);
        }

        public string GenerateUserName(string name, string lastname)
        {
            string _name = char.ToUpper(name[0]) + name.Substring(1);
            string _lastname = char.ToUpper(lastname[0]) + lastname.Substring(1);
            return _name + "_" + _lastname;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
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

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(id, "Doctor/Get/", token._token);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "CollegiateNumber", "User", "Password", "ConfirmedPassword", "Dpi", "Name", "Lastname", "Birthdate", "SpecialtyId")] HGSModel.Doctor updatedDoctor)
        {
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

            if (!updatedDoctor.Password.Equals(updatedDoctor.ConfirmedPassword))
            {
                @ViewData["Response"] = "InconsistentPassword";
            }
            else
            {                
                // Creamos el User del Doctor                
                updatedDoctor.User = GenerateUserName(updatedDoctor.Name, updatedDoctor.Lastname);
                HGSModel.GeneralResult? generalResult = await APIService<HGSModel.GeneralResult>.Update(updatedDoctor, "Doctor/Update", token._token);

                if(generalResult != null)
                {
                    @ViewData["Response"] = generalResult.Message;
                }                
            }

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.SpecialGet("Doctor/GetS", token._token);
            return View(doctor);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
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

            HGSModel.Doctor? doctor = await APIService<HGSModel.Doctor>.Get(id, "Doctor/Get/", token._token);
            return View(doctor);
        }
    }
}